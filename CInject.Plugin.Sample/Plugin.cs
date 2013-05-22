using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CInject.PluginInterface;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Forms;

namespace CInject.Plugin.Sample
{
    [Export(typeof(IPlugin))]
    public class SamplePlugin : IPlugin
    {
        private List<ToolStripItem> _menuItems;
        private ToolStripItem _menuResults;

        public SamplePlugin()
        {
            _menuResults = new ToolStripMenuItem
            {
                Name = "toolStripSample",
                Text = "&Results",
                ShortcutKeys = Keys.Alt | Keys.B,
                ShowShortcutKeys = true
            };
            _menuResults.Click += new EventHandler(_menuResults_Click);
            _menuItems = new List<ToolStripItem> { _menuResults };
        }

        public void OnStart()
        {
            
        }

        public void OnMessageReceived(EventType eventType, PluginInterface.Message message)
        {
            switch (eventType)
            {
                case EventType.ApplicationClosing:
                case EventType.ApplicationStarted:
                    break;
                    // Here Error property will be null
                    // Only Result will be non-null  
                    MessageBox.Show("I received a injection related message: " + message.Result);
                case EventType.TargetAssemblyLoaded:
                case EventType.InjectionAssemblyLoaded:
                    break;
                    // Here Error property will be null
                    // Only Result will be non-null with injector/target assembly path 
                    MessageBox.Show("I received a injection related message: " + message.Result);
                case EventType.MethodInjectionStart:
                case EventType.MethodInjectionComplete:
                    // Here Error property will be null
                    // Expect Target, Injector will be non-null
                    MessageBox.Show("I received a injection related message: " + message.ToString());
                    break;
                case EventType.ProcessComplete:
                case EventType.ProcessStart:
                    // Here Error property will be null
                    // Expect only the Result to be non-null
                    MessageBox.Show("I received a process related message: " + message.Result);
                    break;
                case EventType.Error:
                    MessageBox.Show("I received an error message: " + message.Error);
                    break;
            }
        }

        void _menuResults_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I am clicked from menu!");
        }

        public string Name
        {
            get { return "Sample Plugin"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        ToolStripMenuItem IPlugin.Menu
        {
            get
            {
                ToolStripMenuItem mainMenu = new ToolStripMenuItem("Sample plugin");

                mainMenu.DropDownItems.AddRange(_menuItems.ToArray());
                return mainMenu;
            }
        }


        public void HandleError(Exception exception)
        {
            MessageBox.Show("Unhandled exception caught :" + exception.Message);
        }

        public void OnClose()
        {
            _menuItems = null;
            _menuResults = null;
        }
    }
}