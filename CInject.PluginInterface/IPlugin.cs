using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CInject.PluginInterface
{
    /// <summary>
    /// Interface that all plugins should implement
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Name of the plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Version of the plugin
        /// </summary>
        string Version { get; }

        /// <summary>
        /// This menu will be displayed in CInject
        /// </summary>
        ToolStripMenuItem Menu  { get; }

        /// <summary>
        /// This method will be called whenever an event will be performed by the user
        /// </summary>
        /// <param name="eventType">Type of Event</param>
        /// <param name="message">Message details</param>
        void OnMessageReceived(EventType eventType, Message message);

        /// <summary>
        /// Handles the un-handled error
        /// </summary>
        /// <param name="exception">Unhandled exception passed by CInject application</param>
        void HandleError(Exception exception);

        /// <summary>
        /// Called when the plugin is loaded
        /// </summary>
        void OnStart();

        /// <summary>
        /// Called when the plugin is unloaded (i.e. when the CInject application closes)
        /// </summary>
        void OnClose();
    }
}
