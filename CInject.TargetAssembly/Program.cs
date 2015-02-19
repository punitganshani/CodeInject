#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file Program.cs is part of CInject.TargetAssembly.
// 
// CInject.TargetAssembly is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// 
// CInject.TargetAssembly is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with CInject.TargetAssembly. If not, see http://www.gnu.org/licenses/.
// 
// History:
// ______________________________________________________________
// Created        09-2011              Punit Ganshani

#endregion

using System;

namespace CInject.TargetAssembly
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var testClass = new TestClass("TestInstance");

            Console.WriteLine("Test of integer param to a method. Expected value: 5 Return Value:" + testClass.Add(2, 3));
            Console.WriteLine("Test of double  param to a method. Expected value: 4 Return Value:" + testClass.Subtract(6, 2));

            Console.WriteLine("Test of return value of a method. Expected value: TestInstance  Return Value:" + testClass.GetName());
            Console.WriteLine("Test of generic parameters to a method. Expected value: TestClass  Return Value:" + testClass.GetTypeName<TestClass>());
            Console.WriteLine("Test of generic parameters to a method. Expected value: TestClass  Return Value:" + testClass.GetTypeName<TestClass>(testClass));

            string name = "Punit", name2 = string.Empty;

            Console.WriteLine("Test of ref parameters to a method. Expected value: Punit.appended Return Value:" + testClass.GetRefValue(ref name));
            Console.WriteLine("Value of name (ref)" + name);

            Console.WriteLine("Test of out parameters to a method. Expected value: new Value Return Value:" + testClass.GetOutValue(out name2));
            Console.WriteLine("Value of name (out)" + name2);

            Console.WriteLine("Test of static method. Expected value: NewInstance Return Value:" + TestClass.Create().Name);
            Console.WriteLine("Test of array as parameter to a method. Expected value: 2 Return Value:" + testClass.GetArrayCount(new[] { "new1", "new2" }));

            Console.WriteLine("Test of optional parameter to a method. Expected value: 8 Return Value:" + testClass.AddOptional(3));
            Console.WriteLine("Test of optional parameter to a method. Expected value: 10 Return Value:" + testClass.AddOptional(3, 7));

            Console.WriteLine("Test call of delegate");

            TestClass.MyDelegate delegateDefinition = new TestClass.MyDelegate(DelegateCalled);
            testClass.CallDelegate(delegateDefinition);

            Console.WriteLine("Name is: " + testClass.NameProperty);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static void DelegateCalled()
        {
            Console.WriteLine("Delegate Called, Wonderful!");
        }

    }
}