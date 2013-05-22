using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CInject.PluginInterface
{
    /// <summary>
    /// Events raised by CInject provide more information using Message class
    /// </summary>
    public sealed class Message
    {
        /// <summary>
        /// Represents the logged in user on Windows OS
        /// </summary>
        public string UserId
        {
            get { return Environment.UserName; }
        }

        /// <summary>
        /// Technical Exception if thrown by CInject will be caught here.  This will be not-null only for EventType 'Error'
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// Represents the timestamp of the event occured.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Result is non-null value for all events.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// This represents the target method for EventType MethodInjectionStart and MethodInjectionComplete
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// This represents the injector method for EventType MethodInjectionStart and MethodInjectionComplete
        /// </summary>
        public string Injector { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Result    : {0}", Result).AppendLine();
            builder.AppendFormat("User      : {0}", UserId).AppendLine();
            builder.AppendFormat("Target    : {0}", String.IsNullOrEmpty(Target) ? "N/A" : Target).AppendLine();
            builder.AppendFormat("Injector  : {0}", String.IsNullOrEmpty(Injector) ? "N/A" : Injector).AppendLine();
            builder.AppendFormat("Error     : {0}", Error == null ? "N/A" : Error.Message).AppendLine();
            builder.AppendFormat("TimeStamp : {0}", TimeStamp).AppendLine();
            return builder.ToString();
        }
    }
}