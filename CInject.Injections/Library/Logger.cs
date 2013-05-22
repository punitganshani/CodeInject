#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file Logger.cs is part of CInject.Injections.
// 
// CInject.Injections is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// 
// CInject.Injections is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with CInject.Injections. If not, see http://www.gnu.org/licenses/.
// 
// History:
// ______________________________________________________________
// Created        09-2011              Punit Ganshani

#endregion

using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using System;

namespace CInject.Injections.Library
{
    internal static class Logger
    {
        private static readonly ILog Log;

        static Logger()
        {
            if (File.Exists("LogInject.log4net.xml"))
                XmlConfigurator.Configure(new FileInfo("LogInject.log4net.xml"));
            else
                BasicConfigurator.Configure();

            string loggerName = Assembly.GetEntryAssembly().FullName;
            loggerName = loggerName.Substring(0, loggerName.IndexOf(','));

            Log = LogManager.GetLogger(loggerName);
        }

        public static bool IsDebugEnabled
        {
            get { return Log.IsDebugEnabled; }
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        public static void Info(string message)
        {
            Log.Info(message);
        }

        public static void Error(Exception exception)
        {
            Log.Error(string.Format("An error occured while logging: {0}", exception.ToString()));
        }
    }
}