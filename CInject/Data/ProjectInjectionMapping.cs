#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file frmMain.cs is part of CInject.
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
using System.Text;
using CInject.Extensions;
using CInject.Engine.Extensions;

namespace CInject.Data
{
    public class ProjectInjectionMapping
    {
        public string TargetAssemblyPath { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int MethodParameters { get; set; }

        public string InjectorAssemblyPath { get; set; }
        public string InjectorType { get; set; }

        public ProjectInjectionMapping()
        {

        }

        internal static ProjectInjectionMapping FromInjectionMapping(InjectionMapping mapping)
        {
            ProjectInjectionMapping projMapping = new ProjectInjectionMapping();

            projMapping.ClassName = mapping.Method.DeclaringType.Name;
            projMapping.TargetAssemblyPath = mapping.Assembly.Path;
            projMapping.MethodName = mapping.Method.Name;
            projMapping.MethodParameters = mapping.Method.Parameters.Count;

            projMapping.InjectorAssemblyPath = mapping.Injector.Assembly.GetPath();
            projMapping.InjectorType = mapping.Injector.AssemblyQualifiedName;
            
            return projMapping;
        }
    }
}
