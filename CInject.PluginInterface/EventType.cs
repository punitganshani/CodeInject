using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CInject.PluginInterface
{
    /// <summary>
    /// Types of events that CInject raises
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Called when overall processing starts
        /// </summary>
        ProcessStart = 10,
        /// <summary>
        /// Called before an assembly method is injected
        /// </summary>
        MethodInjectionStart = 20,
        /// <summary>
        /// Called after an assembly method is injected
        /// </summary>
        MethodInjectionComplete = 30,
        /// <summary>
        /// Called when overall processing completes
        /// </summary>
        ProcessComplete = 40,
        /// <summary>
        /// Called when a target assembly is loaded
        /// </summary>
        TargetAssemblyLoaded = 50,
        /// <summary>
        /// Called when an injection assembly is loaded
        /// </summary>
        InjectionAssemblyLoaded = 60,
        /// <summary>
        /// Called when CInject application has started
        /// </summary>
        ApplicationStarted = 70,
        /// <summary>
        /// Called when CInject application is closing
        /// </summary>
        ApplicationClosing = 80,

        /// <summary>
        /// Called when an error occurs
        /// </summary>
        Error = 200
        
        
    }
}
