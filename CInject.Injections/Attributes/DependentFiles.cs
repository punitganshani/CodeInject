using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CInject.Injections.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited=true)]
    public class DependentFilesAttribute : Attribute
    {
        public string[] Files { get; set; }

        public DependentFilesAttribute(params string[] files)
        {
            Files = files;

            if (files.Length == 0)
                Files = new string[0];
        }
    }
}
