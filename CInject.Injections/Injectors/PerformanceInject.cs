using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CInject.Injections.Attributes;
using CInject.Injections.Interfaces;
using CInject.Injections.Library;

namespace CInject.Injections.Injectors
{
    /// <summary>
    /// Injector provided with CInject that logs the method execution time in milliseconds
    /// </summary>
    [DependentFiles("CInject.Injections.dll", "LogInject.log4net.xml", "log4net.dll")]
    public class PerformanceInject : ICInject
    {
        private DateTime _startTime;
        private CInjection _injection;
        private bool _disposed;

        public void OnInvoke(CInjection injection)
        {
            _injection = injection;
            _startTime = DateTime.Now;
        }

        public void OnComplete()
        {
            if (_injection != null && _injection.IsValid())
                Logger.Info(String.Format("{0} executed in {1} mSec",
                    _injection.Method.Name, DateTime.Now.Subtract(_startTime).TotalMilliseconds));
        }

        ~PerformanceInject()
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

        public string[] DependentFiles
        {
            get
            {
                return new string[]
                {
                    "CInject.Injections.dll",
                    "LogInject.log4net.xml",
                    "log4net.dll"
                };
            }
        }
    }
}