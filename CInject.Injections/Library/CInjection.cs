using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CInject.Injections.Library
{
    public class CInjection
    {
        public MethodBase Method { get; set; }

        public Assembly ExecutingAssembly { get; set; }

        public object[] Arguments { get; set; }
    }
}
