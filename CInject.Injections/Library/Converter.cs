using System;
using System.Text;
namespace CInject.Injections.Library
{
    internal class Converter
    {
        internal static string ToString(byte[] characters)
        {
            var encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        internal static Byte[] ToByte(string input)
        {
            var encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(input);
            return byteArray;
        }

        internal static string ToString(object value, string defaultValue)
        {
            return value == null ? defaultValue : Convert.ToString(value);
        }

        internal static object Enum<T>(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        internal static object Enum<T>(long value)
        {
            return (T)System.Enum.Parse(typeof(T), value.ToString(), true);
        }
    }
}
