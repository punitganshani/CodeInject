#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file MessageEventArgs.cs is part of CInject.
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

namespace CInject.Data
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public MessageType MessageType { get; set; }
    }
}