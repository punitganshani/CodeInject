#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file Program.cs is part of CInject.
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
using System.Windows.Forms;
using System.ComponentModel.Composition.Hosting;
using CInject.PluginInterface;

namespace CInject
{
    internal static class Program
    {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Application.Run(new frmMain());
        }

        //public IPlugin Compose()
        //{
        //    var catalog = new AggregateCatalog(); //The special type of catalog that combines a number of catalogs
        //    catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
        //    catalog.Catalogs.Add(new DirectoryCatalog(@".\Plugins", "CInject.Plugin.*.dll"));
            
        //    CompositionContainer container = new CompositionContainer(catalog);

        //    return container.GetExportedObject<IPlugin>();
        //}
    }
}