#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file ICInject.cs is part of CInject.Injections.
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

using CInject.Injections.Library;
using System;
namespace CInject.Injections.Interfaces
{
    public interface ICInject : IDisposable
    {
        void OnInvoke(CInjection injection);
        void OnComplete();
    }
}