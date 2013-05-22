#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file InjectionMapping.cs is part of CInject.
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
using CInject.Engine.Resolvers;
using Mono.Cecil;
using System.Xml.Serialization;
using CInject.Extensions;
using CInject.Engine.Extensions;
using CInject.Engine.Utils;

namespace CInject.Data
{
    [Serializable]
    internal sealed class InjectionMapping
    {
        public InjectionMapping(MonoAssemblyResolver assembly,
                                MethodDefinition method, Type injector)
        {
            Assembly = assembly;
            Method = method;
            Injector = injector;

            MethodName = method.Name;
            InjectorName = injector.FullName;
            AssemblyName = assembly.Assembly.FullName;
        }

        public MonoAssemblyResolver Assembly { get; private set; }
        public MethodDefinition Method { get; private set; }
        public Type Injector { get; private set; }

        public string MethodName { get; private set; }
        public string AssemblyName { get; private set; }
        public string InjectorName { get; private set; }

        public override int GetHashCode()
        {
            return Method.GetHashCode() + Assembly.GetHashCode() + Injector.GetHashCode();
        }

        internal static InjectionMapping FromProjectInjectionMapping(ProjectInjectionMapping projMapping)
        {
            MonoAssemblyResolver targetAssembly = null;
            TypeDefinition type = null;
            MethodDefinition method = null;
            Type injector = null;
            if (CacheStore.Exists<MonoAssemblyResolver>(projMapping.TargetAssemblyPath))
            {
                targetAssembly = CacheStore.Get<MonoAssemblyResolver>(projMapping.TargetAssemblyPath);
            }
            else
            {
                targetAssembly = new MonoAssemblyResolver(projMapping.TargetAssemblyPath);
                CacheStore.Add<MonoAssemblyResolver>(projMapping.TargetAssemblyPath, targetAssembly);
            }

            string classNameKey = targetAssembly.Assembly.Name.Name + "." + projMapping.ClassName;

            if (CacheStore.Exists<TypeDefinition>(classNameKey))
            {
                type = CacheStore.Get<TypeDefinition>(classNameKey);
            }
            else
            {
                type = targetAssembly.Assembly.MainModule.GetType(classNameKey);
                CacheStore.Add<TypeDefinition>(classNameKey, type);
            }

            if (CacheStore.Exists<MethodDefinition>(classNameKey + projMapping.MethodName))
            {
                method = CacheStore.Get<MethodDefinition>(classNameKey + projMapping.MethodName);
            }
            else
            {
                method = type.GetMethodDefinition(projMapping.MethodName, projMapping.MethodParameters);
                CacheStore.Add<MethodDefinition>(classNameKey + projMapping.MethodName, method);
            }

            if (CacheStore.Exists<Type>(projMapping.InjectorType))
            {
                injector = CacheStore.Get<Type>(projMapping.InjectorType);
            }
            else
            {
                injector = Type.GetType(projMapping.InjectorType);
                CacheStore.Add<Type>(projMapping.InjectorType, injector);
            }

            return new InjectionMapping(targetAssembly, method, injector);
        }
    }
}