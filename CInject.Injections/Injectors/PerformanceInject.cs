using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CInject.Injections.Interfaces;
using CInject.Injections.Library;

namespace CInject.Injections.Injectors
{
    /// <summary>
    /// Injector provided with CInject that logs the method execution time in milliseconds
    /// </summary>
    public class PerformanceInject : ICInject
    {
        private DateTime _startTime;
        private CInjection _injection;

        public void OnInvoke(CInjection injection)
        {
            _injection = injection;
            _startTime = DateTime.Now;
        }

        public void OnComplete()
        {
            Logger.Info(String.Format("{0} executed in {1} mSec",
                _injection.Method.Name, DateTime.Now.Subtract(_startTime).TotalMilliseconds));
        }
    }
}