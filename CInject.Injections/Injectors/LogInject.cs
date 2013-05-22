#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file LogInject.cs is part of CInject.Injections.
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

using System;
using CInject.Injections.Interfaces;
using CInject.Injections.Library;
using System.Text;
using System.Collections;

namespace CInject.Injections.Injectors
{
    public class LogInject : ICInject
    {
        private CInjection _injection;
        public LogInject()
        {
            
        }

        #region ICInject Members

        public void OnInvoke(CInjection injection)
        {
            _injection = injection;

            try
            {
                Logger.Info(String.Format("Invoked {0}.{1}, {2}", injection.Method.DeclaringType.Name, injection.Method.Name, injection.ExecutingAssembly.GetName().Name));

                if (Logger.IsDebugEnabled)
                {
                    var parameters = injection.Method.GetParameters();
                    if (injection.Arguments != null && parameters != null)
                    {
                        Logger.Debug(String.Format(">> Paramerters: {0}", injection.Arguments.Length));
                        for (int i = 0; i < injection.Arguments.Length; i++)
                        {
                            if (injection.Arguments[i] is IDictionary)
                            {
                                IDictionary dictionary = (IDictionary)injection.Arguments[i];
                                StringBuilder dictionaryBuilder = new StringBuilder();
                                foreach (var key in dictionary.Keys)
                                {
                                    dictionaryBuilder.AppendFormat("{0}={1}|", key, GetStringValue(dictionary[key]));
                                }

                                Logger.Debug(String.Format("    [{0}]: {1}", parameters[i].Name, dictionaryBuilder.ToString().TrimEnd(new[] { '|' })));
                            }
                            else if (injection.Arguments[i] is ICollection)
                            {
                                ICollection collection = (ICollection)injection.Arguments[i];
                                IEnumerator enumerator = collection.GetEnumerator();
                                StringBuilder dictionaryBuilder = new StringBuilder();

                                while (enumerator.MoveNext())
                                {
                                    dictionaryBuilder.AppendFormat("{0},", GetStringValue(enumerator.Current)).AppendLine();
                                }

                                Logger.Debug(String.Format("    [{0}]: {1}", parameters[i].Name, dictionaryBuilder.ToString().TrimEnd(new[] { ',' })));
                            }
                            else if (injection.Arguments[i] is IEnumerable)
                            {
                                IEnumerable enumerator = (IEnumerable)injection.Arguments[i];
                                StringBuilder dictionaryBuilder = new StringBuilder();

                                foreach (var item in enumerator)
                                {
                                    dictionaryBuilder.AppendFormat("{0},", GetStringValue(item)).AppendLine();
                                }
                                Logger.Debug(String.Format("    [{0}]: {1}", parameters[i].Name, dictionaryBuilder.ToString().TrimEnd(new[] { ',' })));
                            }
                            else
                            {
                                Logger.Debug(String.Format("    [{0}]: {1}", parameters[i].Name, GetStringValue(injection.Arguments[i])));
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        private string GetStringValue(object input)
        {
            if (input == null)
                return "null";

            try
            {
                return CachedSerializer.Serialize(input.GetType(), input, Encoding.UTF8);
            }
            catch // can not serialize, then call ToString() method.
            {
                return input.ToString();
            }
        }

        #endregion

        ~LogInject()
        {
            try
            {
                Logger.Debug(String.Format("Destroyed {0}.{1}, {2}", _injection.Method.DeclaringType.Name, _injection.Method.Name, _injection.ExecutingAssembly.GetName().Name));
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }


        public void OnComplete()
        {
            Logger.Info(String.Format("Completed {0}.{1}, {2}", _injection.Method.DeclaringType.Name, _injection.Method.Name, _injection.ExecutingAssembly.GetName().Name));
        }
    }
}