#region GNU GPL Version 3 License

// Copyright 2011 Punit Ganshani
// 
// This file frmMain.cs is part of CInject.
// 
// CInject is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// 
// CInject is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with CInject. If not, see http://www.gnu.org/licenses/.
// 
// History:
// ______________________________________________________________
// Created        09-2011              Punit Ganshani

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CInject.Data;
using CInject.Engine.Data;
using CInject.Extensions;
using CInject.Engine.Extensions;
using CInject.Injections.Attributes;
using CInject.Injections.Interfaces;
using CInject.Engine.Resolvers;
using CInject.Engine.Utils;
using CInject.Properties;
using CInject.Utils;
using Mono.Cecil;
using BaseAssemblyResolver = CInject.Engine.Resolvers.BaseAssemblyResolver;
using System.Drawing;
using CInject.PluginInterface;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading.Tasks;

namespace CInject
{
    public partial class frmMain : Form
    {
        private bool _injectorAssemblyLoaded;
        private List<InjectionMapping> _mapping;
        private TreeNode _rootInjection;
        private TreeNode _rootTarget;
        private Dictionary<string, MonoAssemblyResolver> _targetAssemblies;
        private bool _targetAssemblyLoaded;
        private Project _project;

        [ImportMany(typeof(IPlugin))]
        public IEnumerable<IPlugin> Plugins { get; set; }

        internal List<IPlugin> PluginList;

        public frmMain()
        {
            InitializeComponent();
            InitializeUI();
            try
            {
                LoadPlugins();
                SendLoadMessageToPlugins();
            }
            catch (Exception ex) { MessageBox.Show(Resources.PluginLoadError + ex.Message); }
        }

        private void SendLoadMessageToPlugins()
        {
            if (Plugins != null)
            {
                Parallel.ForEach(Plugins, (current) =>
                {
                    try
                    {
                        current.OnStart();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            current.HandleError(ex);
                        }
                        catch { }
                    }
                });
                SendMessageToPlugins(EventType.ApplicationStarted, null, null, "Application started successfully!");
            }
        }

        private void LoadPlugins()
        {
            try
            {
                if (!Directory.Exists(@".\Plugins")) return;

                var catalog = new DirectoryCatalog(@".\Plugins", "CInject.Plugin.*.dll");
                catalog.Refresh();

                var container = new CompositionContainer(catalog);
                Plugins = container.GetExportedValues<IPlugin>();

                var pluginArray = Plugins as IPlugin[] ?? Plugins.ToArray();
                PluginList = pluginArray.ToList();

                Parallel.ForEach(pluginArray, (current) =>
                {
                    try
                    {
                        if (current.Menu != null)
                            PopulatePluginMenu(current);
                    }
                    catch
                    {

                    }
                });
            }
            catch (AggregateException aggregateException)
            {
                string message = String.Concat(aggregateException.InnerExceptions.Select(x => x.Message));
                MessageBox.Show(Resources.PluginLoadErrorAggregate + message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(Resources.PluginLoadErrorAggregate + exception.Message);
            }
        }

        private void PopulatePluginMenu(IPlugin plugin)
        {
            menuStrip.Items.Add(plugin.Menu);
        }

        private void InitializeUI()
        {
            treeTarget.Nodes.Clear();
            _rootTarget = new TreeNode { Text = @"Target Assemblies", Tag = null };
            treeTarget.Nodes.Add(_rootTarget);

            treeInjectionCode.Nodes.Clear();
            _rootInjection = new TreeNode { Text = @"Injectors", Tag = null };
            treeInjectionCode.Nodes.Add(_rootInjection);

            _targetAssemblies = new Dictionary<string, MonoAssemblyResolver>();
            _mapping = new List<InjectionMapping>();
            _project = new Project();

            grdCombination.DataSource = null;

            var directory = Path.GetDirectoryName(Application.ExecutablePath);
            if (directory != null)
            {
                var files = Directory.GetFiles(directory, "*.dll");
                foreach (var file in files)
                {
                    LoadInjectorAssembly(file);
                }
            }
        }

        private void resolver_OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler<MessageEventArgs>(resolver_OnMessageReceived), new[] { sender, e });

            lstOutput.Items.Insert(0, String.Format("{0} [{1}] {2}", DateTime.Now.ToString("hh:mm:ss"), e.MessageType, e.Message));
        }

        private void OnBindMethodsDefinitions(MonoAssemblyResolver assemblyTarget, TypeDefinition typeDefinition,
                                              TreeNode rootNode)
        {
            var methodDefinitions = typeDefinition.GetMethods(false);

            for (int i = 0; i < methodDefinitions.Count; i++)
            {
                var node = new TreeNode
                               {
                                   Text =
                                       string.Format("{0} ({1})", methodDefinitions[i].Name,
                                                     methodDefinitions[i].Parameters.Count),
                                   Tag = new BindItem { Method = methodDefinitions[i], Assembly = assemblyTarget }
                               };

                rootNode.Nodes.Add(node);
            }
        }

        private void OnBindInjectors(BaseAssemblyResolver assembly, Type type, TreeNode rootNode, bool staticOnly)
        {
            if (InvokeRequired)
                Invoke(new BindInjections(OnBindInjectors), new object[] { assembly, type, rootNode, staticOnly });

            var node = new TreeNode
                           {
                               Text = type.Name,
                               Tag = type
                           };

            rootNode.Nodes.Add(node);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BindMappings();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (grdCombination.SelectedRows.Count > 0)
            {
                DialogResult dialog =
                    MessageBox.Show(@"This will delete the selected rows from the mappings. Are you sure?",
                                    Application.ProductName, MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    int count = grdCombination.SelectedRows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        object selectedMapping = grdCombination.SelectedRows[i].DataBoundItem;
                        if (selectedMapping != null)
                            _mapping.Remove((InjectionMapping)selectedMapping);
                    }

                    grdCombination.DataSource = null;
                    grdCombination.DataSource = _mapping;
                    grdCombination.Refresh();
                }
            }

            if (_mapping.Count == 0)
                btnRemove.Enabled = false;
        }

        private void treeTarget_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    e.Node.CheckAllChildNodes(e.Node.Checked);
                }
                e.Node.CheckParentNode(e.Node.Checked);
            }
        }

        private void treeInjectionCode_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    e.Node.CheckAllChildNodes(e.Node.Checked);
                }
                e.Node.CheckParentNode(e.Node.Checked);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new frmAbout();
            about.ShowDialog();
        }

        #region Menu

        private void openInjectionAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openInjectionDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string assemblyName = openInjectionDialog.FileName;
                LoadInjectorAssembly(assemblyName);
            }
        }

        private void LoadInjectorAssembly(string assemblyName)
        {
            _project.Injectors.Add(assemblyName);
            AddInjectionAssembly(assemblyName);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openAssemblyDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                for (int i = 0; i < openAssemblyDialog.FileNames.Length; i++)
                {
                    AddTargetAssembly(openAssemblyDialog.FileNames[i]);
                    _project.TargetAssemblies.Add(openAssemblyDialog.FileNames[i]);
                }
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_injectorAssemblyLoaded == false)
                {
                    MessageBox.Show(@"Code Injector assembly not loaded");
                    return;
                }

                if (_targetAssemblyLoaded == false)
                {
                    MessageBox.Show(@"Target Assembly not loaded");
                    return;
                }

                if (BindMappings())
                {
                    if (InjectMappings())
                    {
                        if (SaveAssemblies())
                        {
                            MessageBox.Show(@"Injection completed successfully!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error processing the assembly: " + ex.Message);
                SendMessageToPlugins(EventType.Error, null, ex, "Error while processing assembly");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Parallel.ForEach(Plugins, (current) =>
                {
                    try
                    {
                        current.OnClose();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            current.HandleError(ex);
                        }
                        catch { }
                    }
                });
                SendMessageToPlugins(EventType.ApplicationClosing, null, null, "Application is closing");
            }
            catch { }
            Application.Exit();
        }

        private void selectDirectoryStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = folderBrowserDialog.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                var search = new FileSearch();
                search.SearchExtensions.AddRange(new[] { ".dll", ".exe" });
                var files = search.Search(folderBrowserDialog.SelectedPath);

                for (int x = 0; x < files.Length; x++)
                {
                    AddTargetAssembly(files[x].FullName);
                    _project.TargetAssemblies.Add(files[x].FullName);
                }
            }
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _project = new Project();
            _mapping = new List<InjectionMapping>();

            DialogResult dialogResult = openProjectFileDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Project));
                TextReader reader = new StreamReader(openProjectFileDialog.FileName);

                _project = (Project)serializer.Deserialize(reader);

                _project.Injectors.ForEach(x => AddInjectionAssembly(x));
                _project.TargetAssemblies.ForEach(x => AddTargetAssembly(x));
                _project.Mapping.ForEach(x => _mapping.Add(InjectionMapping.FromProjectInjectionMapping(x)));

                grdCombination.DataSource = _mapping;

                reader.Close();
            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = saveProjectFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                _project.Mapping = new List<ProjectInjectionMapping>();
                // re-convert
                _mapping.ForEach(x => _project.Mapping.Add(ProjectInjectionMapping.FromInjectionMapping(x)));

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Project));
                TextWriter writer = new StreamWriter(saveProjectFileDialog.FileName);

                serializer.Serialize(writer, _project);
                writer.Close();
            }
        }

        #endregion

        #region Processing

        private bool SaveAssemblies()
        {
            try
            {
                IEnumerable<MonoAssemblyResolver> assemblies = _mapping.Select(x => x.Assembly).Distinct();

                foreach (MonoAssemblyResolver assembly in assemblies)
                    assembly.Save();

                return true;
            }
            catch (Exception ex)
            {
                resolver_OnMessageReceived(this, new MessageEventArgs { Message = ex.Message, MessageType = MessageType.Error });
                SendMessageToPlugins(EventType.Error, null, ex, "Error while saving assemblies");
                return false;
            }
        }

        private bool InjectMappings()
        {
            try
            {
                bool success = true;
                SendMessageToPlugins(EventType.ProcessStart, null, null, "Starting injection of " + _mapping.Count + " mappings");
                for (int i = 0; i < _mapping.Count; i++)
                {
                    try
                    {
                        SendMessageToPlugins(EventType.MethodInjectionStart, _mapping[i], null, "Starting injection of a method");
                        success &= _mapping[i].Assembly.Inject(_mapping[i].Method, _mapping[i].Injector);

                        var attributes = _mapping[i].Injector.GetCustomAttributes(true);
                        var directory = Path.GetDirectoryName(_mapping[i].Assembly.Path);
                        if (directory != null)
                        {
                            foreach (var dependentFilesAttribute in attributes)
                            {
                                var typeCasted = dependentFilesAttribute as DependentFilesAttribute;
                                if (typeCasted == null) continue;

                                foreach (var file in typeCasted.Files)
                                {
                                    File.Copy(file, Path.Combine(directory, file), true);
                                }
                            }
                        }

                        SendMessageToPlugins(EventType.MethodInjectionComplete, _mapping[i], null, "Ended injection of a method");
                    }
                    catch (Exception exceptionInjection)
                    {
                        resolver_OnMessageReceived(this, new MessageEventArgs { Message = exceptionInjection.Message, MessageType = MessageType.Error });
                        SendMessageToPlugins(EventType.Error, _mapping[i], exceptionInjection, "Could not process injection");
                    }
                }
                SendMessageToPlugins(EventType.ProcessComplete, null, null, "Completed injection of " + _mapping.Count + " mappings");
                return success;
            }
            catch (Exception ex)
            {
                resolver_OnMessageReceived(this, new MessageEventArgs { Message = ex.Message, MessageType = MessageType.Error });
                SendMessageToPlugins(EventType.Error, null, ex, "Could not process injection");
                return false;
            }
        }

        private void SendMessageToPlugins(EventType eventType, InjectionMapping mapping,
                                          Exception exceptionInjection, string message)
        {
            if (Plugins == null || PluginList == null || PluginList.Count == 0)
                return;

            try
            {
                PluginList.ForEach(x =>
                {
                    try
                    {
                        x.OnMessageReceived(eventType, new PluginInterface.Message
                        {
                            Target = mapping == null ? string.Empty : mapping.Method.Name,
                            Injector = mapping == null ? string.Empty : mapping.Injector.Name,
                            Error = exceptionInjection,
                            Result = message,
                            TimeStamp = DateTime.Now
                        });
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            x.HandleError(ex);
                        }
                        catch
                        {
                            // Ignore error caused due to plugin, later should be given back to IPlugin through
                        }
                    }
                });
            }
            catch { }
        }

        private bool BindMappings()
        {
            List<Type> selectedInjectors = _rootInjection.GetCheckedNodes<Type>();
            Func<BindItem, bool> predicate = x => x.Method != null && x.Method.Body != null;
            List<BindItem> selectedMethods = _rootTarget.GetCheckedNodes<BindItem>(predicate);

            if (_mapping == null)
                _mapping = new List<InjectionMapping>();

            if (selectedInjectors.Count == 0 && _mapping.Count == 0)
            {
                MessageBox.Show(@"No code injectors selected!");
                return false;
            }

            bool injectAllMethods = false;
            if (selectedMethods.Count == 0 && _mapping.Count == 0)
            {
                DialogResult result =
                    MessageBox.Show(
                        @"You have not selected any methods in the Target Assembly that need to be injected!",
                        Application.ProductName, MessageBoxButtons.OK);

                return false;
            }

            if (_mapping.Count > 0)
            {
                DialogResult result =
                    MessageBox.Show(
                        @"You have already defined a mapping, do you want to add this to the mapping or override? Click Yes if you want to add, No if you want to override & Cancel otherwise",
                        Application.ProductName, MessageBoxButtons.YesNoCancel);

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    _mapping = new List<InjectionMapping>();
                    lstOutput.Items.Clear();
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                    return false;
            }

            btnRemove.Enabled = true;

            if (injectAllMethods)
                selectedMethods = _rootTarget.GetNodes<BindItem>(predicate);

            for (int methods = 0; methods < selectedMethods.Count; methods++)
            {
                for (int injector = 0; injector < selectedInjectors.Count; injector++)
                {
                    var newMapping = new InjectionMapping(selectedMethods[methods].Assembly,
                                                          selectedMethods[methods].Method,
                                                          selectedInjectors[injector]);

                    //selectedInjectors[injector]

                    if (_mapping.Count(x => x.GetHashCode() == newMapping.GetHashCode()) == 0)
                        _mapping.Add(newMapping);
                }
            }

            grdCombination.DataSource = null;
            grdCombination.DataSource = _mapping;
            grdCombination.Refresh();

            return true;
        }

        private void AddInjectionAssembly(string assemblyName)
        {
            ReflectionAssemblyResolver assemblyInjectionCode = null;
            if (CacheStore.Exists<ReflectionAssemblyResolver>("injection|" + assemblyName))
            {
                assemblyInjectionCode = CacheStore.Get<ReflectionAssemblyResolver>("injection|" + assemblyName);
            }
            else
            {
                assemblyInjectionCode = new ReflectionAssemblyResolver(assemblyName);
                CacheStore.Add<ReflectionAssemblyResolver>("injection|" + assemblyName, assemblyInjectionCode);
            }

            var nodeAssembly = new TreeNode { Text = Path.GetFileName(assemblyName), Tag = assemblyInjectionCode };

            List<Type> types = assemblyInjectionCode.FindTypes<ICInject>();

            for (int i = 0; i < types.Count; i++)
            {
                OnBindInjectors(assemblyInjectionCode, types[i], nodeAssembly, true);
            }

            // don't show the assembly if it does not have any injectors
            if (types.Count > 0)
            {
                _rootInjection.Nodes.Add(nodeAssembly);
                treeInjectionCode.ExpandAll();
            }

            _injectorAssemblyLoaded = true;
            SendMessageToPlugins(EventType.InjectionAssemblyLoaded, null, null, assemblyName);
        }

        private void AddTargetAssembly(string assemblyName)
        {
            MonoAssemblyResolver assemblyTarget = null;
            if (CacheStore.Exists<MonoAssemblyResolver>("mono|" + assemblyName))
            {
                assemblyTarget = CacheStore.Get<MonoAssemblyResolver>("mono|" + assemblyName);
            }
            else
            {
                assemblyTarget = new MonoAssemblyResolver(assemblyName);
                CacheStore.Add<MonoAssemblyResolver>("mono|" + assemblyName, assemblyTarget);
            }

            // add in the dictionary
            if (_targetAssemblies.ContainsKey(assemblyName) == false)
                _targetAssemblies.Add(assemblyName, assemblyTarget);

            // attach message received event
            assemblyTarget.OnMessageReceived += resolver_OnMessageReceived;

            // add node
            var nodeAssembly = new TreeNode
                                   {
                                       Text = Path.GetFileName(assemblyName),
                                       Tag = new BindItem { Assembly = assemblyTarget, Method = null }
                                   };
            _rootTarget.Nodes.Add(nodeAssembly);

            List<TypeDefinition> types = assemblyTarget.FindClasses();

            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].HasMethods)
                {
                    var nodeClass = new TreeNode
                                        {
                                            Text = types[i].Name,
                                            Tag = types[i]
                                        };

                    nodeAssembly.Nodes.Add(nodeClass);

                    OnBindMethodsDefinitions(assemblyTarget, types[i], nodeClass);
                }
            }

            treeTarget.CollapseAll();

            _targetAssemblyLoaded = true;

            SendMessageToPlugins(EventType.TargetAssemblyLoaded, null, null, assemblyName);
        }

        #endregion

        #region Nested type: BindInjections

        private delegate void BindInjections(BaseAssemblyResolver assembly, Type type, TreeNode node, bool staticOnly);

        #endregion

        private void lstOutput_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            e.DrawBackground();
            e.DrawFocusRectangle();

            string[] parts = lstOutput.Items[e.Index].ToString().Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

            // 0 - time
            // 1 - Message Type
            // 2 - Message
            MessageType messageType = (MessageType)Enum.Parse(typeof(MessageType), parts[1], true);

            Font font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.Black);
            switch (messageType)
            {
                case MessageType.Error:
                    brush.Color = Color.Red;
                    break;
                case MessageType.Warning:
                    brush.Color = Color.DarkOrange;
                    break;
                case MessageType.Output:
                    brush.Color = Color.Green;
                    break;
            }

            e.Graphics.DrawString(lstOutput.Items[e.Index].ToString(), font, brush, e.Bounds);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeUI();
        }
    }
}