using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace CInject.Injections.Library
{
    internal static class CachedSerializer
    {
        private static readonly Dictionary<Type, XmlSerializer> _serializer;
        static CachedSerializer()
        {
            _serializer = new Dictionary<Type, XmlSerializer>();
        }

        private static XmlSerializer GetSerializer<T>()
        {
            Type type = typeof(T);
            if (_serializer.ContainsKey(type))
                return _serializer[type];
            else
            {
                var xs = new XmlSerializer(type);
                _serializer[type] = xs;
                return xs;
            }
        }

        private static XmlSerializer GetSerializer(Type type)
        {
            if (_serializer.ContainsKey(type))
                return _serializer[type];
            else
            {
                var xs = new XmlSerializer(type);
                _serializer[type] = xs;
                return xs;
            }
        }

        public static string Serialize(Type type, object obj, Encoding encoding)
        {
            try
            {

                string xmlString = null;
                var memoryStream = new MemoryStream();
                XmlSerializer xs = GetSerializer(type);
                var xmlTextWriter = new XmlTextWriter(memoryStream, encoding);
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = Converter.ToString(memoryStream.ToArray());
                return xmlString;
            }
            catch
            {
                throw;
            }
        }

        public static string Serialize<T>(T obj, Encoding encoding)
        {
            try
            {

                string xmlString = null;
                var memoryStream = new MemoryStream();
                XmlSerializer xs = GetSerializer<T>();
                var xmlTextWriter = new XmlTextWriter(memoryStream, encoding);
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = Converter.ToString(memoryStream.ToArray());
                return xmlString;
            }
            catch 
            {
                throw;
            }
        }

        public static T Deserialize<T>(string xml, Encoding encoding)
        {
            XmlSerializer xs = GetSerializer<T>();
            var memoryStream = new MemoryStream(Converter.ToByte(xml));
            var xmlTextWriter = new XmlTextWriter(memoryStream, encoding);
            return (T)xs.Deserialize(memoryStream);
        }

        public static object Deserialize(Type type, string xml, Encoding encoding)
        {
            XmlSerializer xs = GetSerializer(type);
            var memoryStream = new MemoryStream(Converter.ToByte(xml));
            var xmlTextWriter = new XmlTextWriter(memoryStream, encoding);
            return xs.Deserialize(memoryStream);
        }


    }
}
