#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file MonoExtensions.cs is part of CInject.
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
using CInject.Extensions;
using Mono.Cecil.Cil;
using Mono.Cecil;

namespace CInject.Utils
{
    public class MethodInjector
    {
        private readonly ILProcessor _ilprocessor;
        private readonly MethodBody _methodBody;

        public MethodBody MethodBody
        {
            get { return _methodBody; }
        } 

        public MethodInjector(MethodDefinition method)
        {
            _methodBody = method.Body;
            _ilprocessor = method.Body.GetILProcessor();
        }

        public Instruction Create(OpCode opcode, VariableDefinition variable)
        {
            return _ilprocessor.Create(opcode, variable);
        }

        public void InsertBefore(Instruction target, Instruction instruction)
        {
            _ilprocessor.InsertBefore(target, instruction);
            UpdateReferences(target, instruction); // shift the instructions and offsets & handlers down
        }

        private void UpdateReferences(Instruction instruction, Instruction replaceBy)
        {
            if (_methodBody.Instructions.Count > 0)
                _methodBody.Instructions.UpdateReferences(instruction, replaceBy);

            if (_methodBody.ExceptionHandlers.Count > 0)
                _methodBody.ExceptionHandlers.UpdateReferences(instruction, replaceBy);
        }

        internal Instruction Create(OpCode opCode, ParameterDefinition parameterDefinition)
        {
            return _ilprocessor.Create(opCode, parameterDefinition);
        }

        internal Instruction Create(OpCode opCode)
        {
            return _ilprocessor.Create(opCode);
        }

        internal Instruction Create(OpCode opCode, int value)
        {
            return _ilprocessor.Create(opCode, value);
        }

        public Instruction Create(OpCode opCode, MethodReference reference)
        {
            return _ilprocessor.Create(opCode, reference);
        }

        public Instruction Create(OpCode opCode, TypeReference reference)
        {
            return _ilprocessor.Create(opCode, reference);
        }

        public Instruction Create(OpCode opCode, FieldReference reference)
        {
            return _ilprocessor.Create(opCode, reference);
        }

        public VariableDefinition AddVariable(TypeReference type)
        {
            _methodBody.InitLocals = true;

            VariableDefinition variable = new VariableDefinition(type);
            _methodBody.Variables.Add(variable);

            return variable;
        }

        //public PropertyDefinition AddProperty(string name, TypeReference type)
        //{
        //    PropertyDefinition property = new PropertyDefinition(name, PropertyAttributes.HasDefault, type);
        //    _ilprocessor.Create(OpCodes.
        //}
    }
}
