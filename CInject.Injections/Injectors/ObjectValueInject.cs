using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CInject.Injections.Attributes;
using CInject.Injections.Interfaces;
using CInject.Injections.Library;
using System.IO;

namespace CInject.Injections.Injectors
{
    [DependentFiles("ObjectSearch.xml", "CInject.Injections.dll", "LogInject.log4net.xml", "log4net.dll")]
    public class ObjectValueInject : ICInject
    {
        public class ObjectSearch
        {
            public string[] PropertyNames;
        }

        private const string FileName = "ObjectSearch.xml";
        private CInjection _injection;
        private bool _disposed;

        public void OnInvoke(CInjection injection)
        {
            _injection = injection;

            
            if (!Logger.IsDebugEnabled) return;
            if (_injection == null) return;
            if (!File.Exists(FileName)) return;
            if (!injection.IsValid()) return;

            var objectSearch = CachedSerializer.Deserialize<ObjectSearch>(File.ReadAllText(FileName), Encoding.UTF8);
            if (objectSearch == null || objectSearch.PropertyNames == null) return;

            foreach (string propertyName in objectSearch.PropertyNames)
            {
                var dictionary = _injection.GetPropertyValue(propertyName);

                foreach (var key in dictionary.Keys)
                {
                    Logger.Debug(String.Format("Method {0} Argument #{1} :{2}= {3}", injection.Method.Name, key, propertyName, dictionary[key] ?? "<null>"));
                }
            }
        }

        public void OnComplete()
        {
            //if (_injection != null && _injection.IsValid())
            //    Logger.Info(String.Format("{0} executed in {1} mSec",
            //        _injection.Method.Name, DateTime.Now.Subtract(_startTime).TotalMilliseconds));
        }

        ~ObjectValueInject()
        {
            if (_disposed) return;

            DestroyObject();
        }

        private void DestroyObject()
        {
            _injection = null;
        }

        public void Dispose()
        {
            _injection = null;
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}