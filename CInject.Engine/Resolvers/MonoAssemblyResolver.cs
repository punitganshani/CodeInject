#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file MonoAssemblyResolver.cs is part of CInject.
// 
// CInject is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// 
// CInject is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with CInject. If not, see http://www.gnu.org/licenses/.
// 
// History:
// ______________________________________________________________
// Created        09-2011              Punit Ganshani

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CInject.Engine.Data;
using CInject.Engine.Extensions;
using CInject.Injections.Interfaces;
using CInject.Engine.Utils;
using Mono.Cecil;
using Mono.Cecil.Cil;
using CInject.Injections.Library;
using System.Diagnostics;

namespace CInject.Engine.Resolvers
{
    public class MonoAssemblyResolver : BaseAssemblyResolver
    {
        private AssemblyDefinition _assembly;

        private TypeReference _cinjection;
        private MethodReference _cinjectionCtor;
        private MethodReference _methodGetCurrentMethod;
        private MethodReference _methodSetMethod;
        private MethodReference _methodGetExecutingAssembly;
        private MethodReference _methodSetExecutingAssembly;

        private MethodReference _methodGetArguments;
        private MethodReference _methodSetArguments;

        public MonoAssemblyResolver(string path)
            : base(path)
        {
            string directory = System.IO.Path.GetDirectoryName(path);
            
            DefaultAssemblyResolver assemblyResolver = new DefaultAssemblyResolver();
            assemblyResolver.AddSearchDirectory(directory);

            ReaderParameters readerParams = new ReaderParameters { AssemblyResolver = assemblyResolver };

            _assembly = AssemblyDefinition.ReadAssembly(path, readerParams);

            _cinjection = Assembly.ImportType<CInjection>();
            _cinjectionCtor = Assembly.ImportConstructor<CInjection>();

            _methodGetCurrentMethod = Assembly.ImportMethod<System.Reflection.MethodBase>("GetCurrentMethod");
            _methodSetMethod = Assembly.ImportMethod<CInjection>("set_Method");

            _methodGetExecutingAssembly = Assembly.ImportMethod<System.Reflection.Assembly>("GetExecutingAssembly");
            _methodSetExecutingAssembly = Assembly.ImportMethod<CInjection>("set_ExecutingAssembly");

            _methodGetArguments = Assembly.ImportMethod<CInjection>("get_Arguments");
            _methodSetArguments = Assembly.ImportMethod<CInjection>("set_Arguments");
        }

        public AssemblyDefinition Assembly
        {
            get { return _assembly; }
            internal set { _assembly = value; }
        }

        public List<TypeDefinition> FindTypes<T1>() where T1 : ICInject
        {
            var injectionTypes = new List<TypeDefinition>();

            foreach (var type in _assembly.MainModule.Types)
            {
                if (type.Interfaces.Count(x => x.FullName == "CInject.Injections.Interfaces.ICInject") > 0)
                {
                    injectionTypes.Add(type);
                }
            }

            return injectionTypes;
        }

        internal List<TypeDefinition> FindStaticClasses()
        {
            return _assembly.MainModule.Types.Where(type => type.Methods.Count(x => x.IsStatic) > 0).ToList();
        }

        public  List<TypeDefinition> FindClasses()
        {
            return _assembly.MainModule.Types.Where(x => x.IsClass).ToList();
        }

        public  bool Inject(Type type)
        {
            if (Assembly.MainModule.GetRuntime() != type.Assembly.GetRuntime())
            {
                SendMessage("Injector and Target Assembly have different CLR versions! Can not proceed.", MessageType.Error);
                return false;
            }

            var injection = CreateInjection(type);

            _assembly.MainModule.Import(injection.InjectionType);

            return PatchModules(_assembly.Modules, injection);
        }

        private Injection CreateInjection(Type type)
        {
            var injection = new Injection
            {
                InjectionType = type,
                OnInvoke = Assembly.ImportMethod(type, "OnInvoke"),
                Constructor = Assembly.ImportConstructor(type),
                TypeReference = Assembly.ImportType(type),
                OnComplete = Assembly.ImportMethod(type, "OnComplete"),
            };
            return injection;
        }

        private bool PatchModules(IEnumerable<ModuleDefinition> collection, Injection injection)
        {
            bool success = true;
            foreach (var module in collection)
            {
                success &= PatchTypes(module.Types, injection);
            }
            return success;
        }

        private bool PatchTypes(IEnumerable<TypeDefinition> collection, Injection injection)
        {
            bool success = true;
            foreach (var type in collection)
            {
                success &= PatchMethods(type.Methods, injection);
            }
            return success;
        }

        public bool Inject(MethodDefinition methodDefinition, Type type)
        {
            bool success = true;
            if (Assembly.MainModule.GetRuntime() != type.Assembly.GetRuntime())
            {
                SendMessage("Injector and Target Assembly have different CLR versions! Can not proceed.", MessageType.Error);
                return false;
            }

            var injection = CreateInjection(type);

            try
            {
                _assembly.MainModule.Import(injection.InjectionType);
            }
            catch
            {

            }

            return PatchMethod(methodDefinition, injection);
        }

        private bool PatchMethods(IEnumerable<MethodDefinition> collection, Injection injection)
        {
            bool success = true;
            foreach (var method in collection)
            {
                success &= PatchMethod(method, injection);
            }
            return success;
        }

        private bool PatchMethod(MethodDefinition method, Injection injection)
        {
            bool success = true;
            if (method.IsConstructor ||
                method.IsAbstract ||
                method.IsSetter ||
                (method.IsSpecialName && !method.IsGetter) || // to allow getter methods
                method.IsGenericInstance ||
                method.IsManaged == false ||
                method.Body == null)
            {
                SendMessage("Ignored method: " + method.Name, MessageType.Warning);
                return true;
            }

            try
            {
                var constructor = injection.Constructor;
                constructor.Resolve();

                bool isInjected = method.Body.Variables.Count(x => x.VariableType.Scope == injection.TypeReference.Scope
                                                                && x.VariableType.FullName == injection.TypeReference.FullName
                                                                && x.VariableType.Namespace == injection.TypeReference.Namespace) > 0;

                if (isInjected) // already injected
                {
                    SendMessage("Already injected method " + method.Name + " with " + injection.TypeReference.Name, MessageType.Warning);
                    return true;
                }

                MethodInjector editor = new MethodInjector(method);

                VariableDefinition vInject = editor.AddVariable(injection.TypeReference);
                VariableDefinition vInjection = editor.AddVariable(_cinjection);
                VariableDefinition vObjectArray = editor.AddVariable(Assembly.ImportType<object[]>());

                Instruction firstExistingInstruction = method.Body.Instructions[0];

                // create constructor of Injector
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Newobj, constructor));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Stloc_S, vInject));

                #region OnInvoke without Param
                //editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldloc_S, vInject));
                //editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Callvirt, injection.OnInvoke));
                #endregion

                #region OnInvoke with Param
                // create constructor of CInjection
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Newobj, _cinjectionCtor));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Stloc_S, vInjection));

                // create parameter of GetCurrentMethod
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldloc_S, vInjection));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Call, _methodGetCurrentMethod));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Callvirt, _methodSetMethod));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Nop));

                // create parameter of GetExecutingAssembly
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldloc_S, vInjection));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Call, _methodGetExecutingAssembly));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Callvirt, _methodSetExecutingAssembly));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Nop));

                if (method.Parameters.Count > 0)
                {
                    // create array of object (arguments)
                    editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldloc_S, vInjection));
                    editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldc_I4, method.Parameters.Count));
                    editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Newarr, Assembly.ImportType<object>()));
                    editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Stloc_S, vObjectArray));

                    for (int i = 0; i < method.Parameters.Count; i++)
                    {
                        bool processAsNormal = true;

                        if (method.Parameters[i].ParameterType.IsByReference)
                        {
                            /* Sample Instruction set:
                             * L_002a: ldloc.2 
                             * L_002b: ldc.i4.0 
                             * L_002c: ldarg.1 
                             * L_002d: ldind.ref 
                             * L_002e: stelem.ref 
                             * */

                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldloc_S, vObjectArray));
                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldc_I4, i));
                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldarg, method.Parameters[i]));
                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldind_Ref));
                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Stelem_Ref));

                            processAsNormal = false;
                        }
                        //else if (method.Parameters[i].ParameterType.IsArray)
                        //{

                        //}
                        //else if (method.Parameters[i].ParameterType.IsDefinition) // delegate needs no seperate handling
                        //{

                        //}
                        else if (method.Parameters[i].ParameterType.IsFunctionPointer)
                        {

                        }
                        //else if (method.Parameters[i].ParameterType.IsOptionalModifier)
                        //{

                        //}
                        else if (method.Parameters[i].ParameterType.IsPointer)
                        {

                        }
                        else
                        {
                            processAsNormal = true;
                        }

                        if (processAsNormal)
                        {
                            /* Sample Instruction set: for simple PARAMETER
                             * L_0036: ldloc.s objArray
                             * L_0038: ldc.i4 0
                             * L_003d: ldarg array
                             * L_0041: box Int32    <-------------- anything can be here
                             * L_0046: stelem.ref 
                             * */

                            /* Sample Instruction set: for ARRAY
                             * L_0036: ldloc.s objArray
                             * L_0038: ldc.i4 0
                             * L_003d: ldarg array
                             * L_0041: box string[]
                             * L_0046: stelem.ref 
                             * */

                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldloc_S, vObjectArray));
                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldc_I4, i));
                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldarg, method.Parameters[i]));
                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Box, method.Parameters[i].ParameterType));
                            editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Stelem_Ref));
                        }
                    }

                    editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldloc_S, vObjectArray));
                    editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Callvirt, _methodSetArguments));
                }

                // call OnInvoke with appropriate parameters
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldloc_S, vInject));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Ldloc_S, vInjection));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Callvirt, injection.OnInvoke));
                editor.InsertBefore(firstExistingInstruction, editor.Create(OpCodes.Nop));

                #endregion

                #region OnComplete
                Instruction[] exitInstructions = method.Body.Instructions.Where(x => x.OpCode == OpCodes.Ret).ToArray();

                for (int i = 0; i < exitInstructions.Length; i++)
                {
                    var previous = exitInstructions[i].Previous; // most likely previous statement will be NOP, LDLOC.0 (pop, or load from stack)
                    editor.InsertBefore(previous, editor.Create(OpCodes.Ldloc_S, vInject));
                    editor.InsertBefore(previous, editor.Create(OpCodes.Callvirt, injection.OnComplete));

                    Debug.WriteLine(method.Name + " " + method.ReturnType.Name + " " + previous.OpCode);
                }
                #endregion

                method.Resolve();

                SendMessage("Injected method " + method, MessageType.Output);
            }
            catch (Exception ex)
            {
                SendMessage(ex.Message, MessageType.Error);
                return false;
            }

            return true;
        }

        private void FindExitStatements(MethodInjector editor, Injection injection)
        {
            //TODO: Find Exit Statements

        }

        public bool Save()
        {
            try
            {
                _assembly.Write(Path, new WriterParameters { WriteSymbols = false });
                SendMessage("New assembly saved " + Path, MessageType.Output);
                return true;
            }
            catch (Exception ex)
            {
                SendMessage(ex.Message, MessageType.Error);
                return false;
            }
        }
    }
}