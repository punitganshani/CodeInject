#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file FileSearch.cs is part of CInject.
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

using System.Collections;
using System.IO;

namespace CInject.Utils
{
    public class FileSearch
    {
        private readonly ArrayList _extensions;
        private bool _recursive;

        public FileSearch()
        {
            _extensions = ArrayList.Synchronized(new ArrayList());
            _recursive = true;
        }

        public ArrayList SearchExtensions
        {
            get { return _extensions; }
        }

        public bool Recursive
        {
            get { return _recursive; }
            set { _recursive = value; }
        }

        public FileInfo[] Search(string path)
        {
            var root = new DirectoryInfo(path);
            var subFiles = new ArrayList();
            foreach (FileInfo file in root.GetFiles())
            {
                if (_extensions.Contains(file.Extension))
                {
                    subFiles.Add(file);
                }
            }
            if (_recursive)
            {
                foreach (DirectoryInfo directory in root.GetDirectories())
                {
                    subFiles.AddRange(Search(directory.FullName));
                }
            }
            return (FileInfo[]) subFiles.ToArray(typeof (FileInfo));
        }
    }
}