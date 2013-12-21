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

        public bool IsValid()
        {
            return Method != null
                    && ExecutingAssembly != null
                    && Method.DeclaringType != null;
        }

        public string GetMessage(string prefix)
        {
            if (!IsValid()) return string.Empty;

            return String.Format("{0} {1}.{2}, {3}", prefix, Method.DeclaringType.Name, Method.Name,
                ExecutingAssembly.GetName().Name);
        }

        /// <summary>
        /// If the argument in the CInjection is a complex object (any class actually), you can get the value of
        /// its property
        /// </summary>
        /// <param name="argumentIndex">Index of Argument</param>
        /// <param name="propertyName">Name of property</param>
        /// <exception cref="InvalidOperationException">If the argumentIndex does not exist</exception>
        /// <returns>Value of the property. If property is not found, it will return NULL</returns>
        public object GetPropertyOfArgument(int argumentIndex, string propertyName)
        {
            if (Arguments == null) return null;

            if (Arguments.Length <= argumentIndex)
                throw new InvalidOperationException("ArgumentIndex does not exist in the Injection :" + argumentIndex);

            object argumentRequired = Arguments[argumentIndex];

            if (argumentRequired != null)
                return argumentRequired.GetType().GetProperty(propertyName).GetValue(argumentRequired, null);

            return null;
        }

        /// <summary>
        /// Scans all arguments for a PropertyName
        /// </summary>
        /// <param name="propertyName">PropertyName by which the method has to search</param>
        /// <returns>dictionary of argument index and property name</returns>
        public Dictionary<int, object> GetPropertyValue(string propertyName)
        {
            var argumentsAndPropertyValues = new Dictionary<int, object>();
            if (Arguments == null) return argumentsAndPropertyValues;

            for (int i = 0; i < Arguments.Length; i++)
            {
                var propertyValue = GetPropertyOfArgument(i, propertyName);
                if (propertyValue != null)
                {
                    argumentsAndPropertyValues.Add(i, propertyValue);
                }
            }

            return argumentsAndPropertyValues;
        }
    }
}
