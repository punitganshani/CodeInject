using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CInject.Injections.Injectors;
using CInject.Injections.Library;
using System.Threading;

namespace CInject.TargetAssembly
{
    public class TestClass
    {
        public string Name { get; set; }
        public delegate void MyDelegate();

        public TestClass(string name)
        {
            this.Name = name;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetTypeName<T>()
        {
            return typeof(T).Name;
        }

        public string GetTypeName<T>(T obj)
        {
            return obj.GetType().Name;
        }

        public string GetStringValue(string name)
        {
            return "Work " + name;
        }


        public string GetRefValue(ref string name)
        {
            try
            {
                LogInject inject = new LogInject();
                CInjection injection = new CInjection();
                injection.Method = System.Reflection.MethodBase.GetCurrentMethod();
                injection.ExecutingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                injection.Arguments = new object[] { name };
                inject.OnInvoke(injection);
            }
            catch { }
            Sleep();
            name += ".appended";
            return name;
        }

        public int GetArrayCount(string[] array)
        {
            //LogInject inject = new LogInject();
            //CInjection injection = new CInjection();
            //injection.Method = System.Reflection.MethodBase.GetCurrentMethod();
            //injection.ExecutingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            //injection.Arguments = new object[] { array};
            //inject.OnInvoke(injection);

            if (array != null)
                return array.Length;

            return 0;
        }

        public int AddOptional(int x, int y = 5)
        {
            //LogInject inject = new LogInject();
            //CInjection injection = new CInjection();
            //injection.Method = System.Reflection.MethodBase.GetCurrentMethod();
            //injection.ExecutingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            //injection.Arguments = new object[] { x, y };
            //inject.OnInvoke(injection);

            return x + y;
        }

        public string GetOutValue(out string name)
        {
            name = "new Value";
            return name;
        }

        public void CallDelegate(MyDelegate d)
        {
            d(); // call the delegate that was passed in
        }

        public void Sleep()
        {
            Thread.Sleep(1200);
        }

        public static TestClass Create()
        {

            return new TestClass("NewInstance");
        }

        public int Add(int x, int y)
        {
            Sleep();
            return x + y;
        }

        public double Subtract(double x, double y)
        {
            return x - y;
        }

        public string NameProperty
        {
            get
            {
                return Name;
            }
        }
    }
}
