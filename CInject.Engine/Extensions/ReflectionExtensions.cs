#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file ReflectionExtensions.cs is part of CInject.
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
using System.Reflection;
using CInject.Engine.Data;
using System.IO;

namespace CInject.Engine.Extensions
{
    public static class ReflectionExtensions
    {
        public static List<Type> GetType<T>(this Assembly assembly)
        {
            //TODO: Optimize this...
            Type[] types = assembly.GetTypes();
            Type interfaceType = typeof(T);

            //does not work!
            //return types.Where(x => x.IsAssignableFrom(interfaceType)).ToList();

            var selected = new List<Type>();

            for (int x = 0; x < types.Length; x++)
            {
                var s1 = types[x].GetInterfaces();
                var counter = s1.Count(y => y.FullName == interfaceType.FullName);

                if (counter > 0)
                    selected.Add(types[x]);
            }

            return selected;
        }

        public static Runtime GetRuntime(this Assembly assembly)
        {
            if (assembly.ImageRuntimeVersion.Contains("v1.0"))
                return Runtime.Net_1_0;
            else if (assembly.ImageRuntimeVersion.Contains("v1.1"))
                return Runtime.Net_1_1;
            else if (assembly.ImageRuntimeVersion.Contains("v2.0")
                    || assembly.ImageRuntimeVersion.Contains("v3.0")
                    || assembly.ImageRuntimeVersion.Contains("v3.5"))
                return Runtime.Net_2_0;
            else
                return Runtime.Net_4_0;
        }

        public static string GetPath(this Assembly assembly)
        {
            return new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
        }
    }
}