#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file MonoExtensions.cs is part of CInject.
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
using System.Linq;
using Mono.Cecil.Cil;
using Mono.Cecil;
using CInject.Engine.Data;
using CInject.Injections.Library;
using System.Reflection;

namespace CInject.Engine.Extensions
{
    public static class MonoExtensions
    {
        public static MethodReference ImportMethod<T>(this AssemblyDefinition assembly, string methodName,
                                                      System.Reflection.BindingFlags bindingFlags)
        {
            return assembly.MainModule.Import(typeof(T).GetMethod(methodName, bindingFlags));
        }

        public static MethodReference ImportMethod<T>(this AssemblyDefinition assembly, string methodName, Type[] types)
        {
            return assembly.MainModule.Import(typeof(T).GetMethod(methodName, types));
        }

        public static MethodReference ImportMethod(this AssemblyDefinition assembly, Type type, string methodName, Type[] types)
        {
            var methods = type.GetMethods().Count(x => x.Name == methodName);

            if (methods > 1)
                throw new AmbiguousMatchException("More than one method with name " + methodName + " found in " + type.Name);
            else
                return assembly.MainModule.Import(type.GetMethod(methodName, types));
        }

        public static MethodReference ImportMethod<T>(this AssemblyDefinition assembly, string methodName)
        {
            return assembly.MainModule.Import(typeof(T).GetMethod(methodName));
        }

        public static MethodReference ImportPropertyGetter<T>(this AssemblyDefinition assembly, string propertyName)
        {
            return assembly.MainModule.Import(typeof(T).GetProperty(propertyName).GetGetMethod());
        }

        public static MethodReference ImportMethod(this AssemblyDefinition assembly, Type type, string methodName)
        {
            var input = type.GetMethod(methodName);
            return assembly.MainModule.Import(input);
        }

        public static TypeReference ImportType<T>(this AssemblyDefinition assembly)
        {
            return assembly.MainModule.Import(typeof(T));
        }

        public static TypeReference ImportType(this AssemblyDefinition assembly, Type type)
        {
            return assembly.MainModule.Import(type);
        }

        public static MethodReference ImportConstructor<T>(this AssemblyDefinition assembly, params Type[] types)
        {
            return assembly.MainModule.Import(typeof(T).GetConstructor(types));
        }

        public static MethodReference ImportConstructor<T>(this AssemblyDefinition assembly)
        {
            Type inputType = typeof(T);
            return assembly.MainModule.Import(inputType.GetConstructors().First(c => !c.IsStatic));
        }

        public static MethodReference ImportConstructor(this AssemblyDefinition assembly, Type inputType)
        {
            return assembly.MainModule.Import(inputType.GetConstructors().First(c => !c.IsStatic));
        }

        public static MethodDefinition GetMethodDefinition(this TypeDefinition typeDefinition, string name,
                                                           int paramcount)
        {
            foreach (MethodDefinition mdef in typeDefinition.Methods)
            {
                // demo purpose only
                if ((mdef.Name == name) && (paramcount == mdef.Parameters.Count))
                    return mdef;
            }
            throw new ArgumentException("Unable to find this method!");
        }

        public static List<MethodDefinition> GetMethods(this TypeDefinition typeDefinition, bool showConstructor)
        {
            if (showConstructor)
                return typeDefinition.Methods.ToList();
            else
            {
                List<MethodDefinition> actualMethods = typeDefinition.Methods.ToList();

                for (int i = actualMethods.Count - 1; i != 0; i--)
                {
                    if (actualMethods[i].IsConstructor)
                    {
                        actualMethods.Remove(actualMethods[i]);
                    }
                }

                return actualMethods;
            }
        }

        public static ParameterDefinition GetParameter(this Mono.Cecil.Cil.MethodBody inputMethod, int index)
        {
            MethodDefinition method = inputMethod.Method;
            if (method.HasThis)
            {
                if (index == 0)
                    return inputMethod.ThisParameter;
                index--;
            }
            return method.Parameters[index];
        }

        public static void UpdateReferences(this IEnumerable<Instruction> collection, Instruction oldTarget, Instruction newTarget)
        {
            foreach (var currentInstruction in collection)
            {
                if (currentInstruction.OpCode == OpCodes.Switch)
                {
                    var labels = (Instruction[])currentInstruction.Operand;
                    labels.ReplaceAll(oldTarget, newTarget);
                }
                else if (currentInstruction.Operand == oldTarget)
                {
                    currentInstruction.Operand = newTarget;
                }
            }
        }

        public static void UpdateReferences(this IEnumerable<ExceptionHandler> handlers, Instruction oldTarget, Instruction newTarget)
        {
            foreach (var handler in handlers)
            {
                if (handler.TryEnd == oldTarget)
                    handler.TryEnd = newTarget;
                if (handler.TryStart == oldTarget)
                    handler.TryStart = newTarget;
                if (handler.HandlerStart == oldTarget)
                    handler.HandlerStart = newTarget;
                if (handler.FilterStart == oldTarget)
                    handler.FilterStart = newTarget;
            }
        }

        public static void ReplaceAll(this Instruction[] labels, Instruction oldTarget, Instruction newTarget)
        {
            for (int i = 0; i < labels.Length; ++i)
            {
                if (labels[i] == oldTarget)
                {
                    labels[i] = newTarget;
                }
            }
        }

        public static Runtime GetRuntime(this ModuleDefinition module)
        {
            switch (module.Runtime)
            {
                case TargetRuntime.Net_1_0:
                    return Runtime.Net_1_0;

                case TargetRuntime.Net_1_1:
                    return Runtime.Net_1_1;

                case TargetRuntime.Net_2_0:
                    return Runtime.Net_2_0;

                default:
                case TargetRuntime.Net_4_0:
                    return Runtime.Net_4_0;
            }
        }

    }
}