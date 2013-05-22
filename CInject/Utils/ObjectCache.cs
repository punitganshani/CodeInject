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
using CInject.Data;

namespace CInject.Utils
{
    public static class ObjectCache
    {
        private static Dictionary<string, object> _cacheObjects;
        static ObjectCache()
        {
            _cacheObjects = new Dictionary<string, object>();
        }

        public static T Add<T>(string key, T data)
        {
            lock (_cacheObjects)
            {
                if (_cacheObjects.ContainsKey(key))
                {
                    lock (_cacheObjects)
                    {
                        _cacheObjects[key] = data;
                    }
                }
                else
                {
                    lock (_cacheObjects)
                    {
                        _cacheObjects.Add(key, data);
                    }
                }
            }

            return data;
        }

        public static bool ContainsKey(string key)
        {
            lock (_cacheObjects)
            {
                return _cacheObjects.ContainsKey(key);
            }
        }

        public static T Get<T>(string key)
        {
            if (_cacheObjects.ContainsKey(key))
                return (T)_cacheObjects[key];

            throw new KeyNotFoundException(@"Key not found:" + key);
        }
    }
}
