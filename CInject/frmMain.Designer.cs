using CInject.Engine.Utils;
using CInject.Utils;
namespace CInject
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDirectoryStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInjectionAssemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAssemblyDialog = new System.Windows.Forms.OpenFileDialog();
            this.openInjectionDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeTarget = new CInject.Utils.CInjectTreeView();
            this.treeInjectionCode = new CInject.Utils.CInjectTreeView();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.grdCombination = new System.Windows.Forms.DataGridView();
            this.assemblyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.injectorNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openProjectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveProjectFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tblMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCombination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectMenu,
            this.injectToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(745, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectDirectoryStripMenuItem,
            this.toolStripMenuItem3,
            this.openToolStripMenuItem,
            this.openInjectionAssemblyToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.fileToolStripMenuItem.Text = "Asse&mbly";
            // 
            // selectDirectoryStripMenuItem
            // 
            this.selectDirectoryStripMenuItem.Name = "selectDirectoryStripMenuItem";
            this.selectDirectoryStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.selectDirectoryStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.selectDirectoryStripMenuItem.Text = "Select &Directory";
            this.selectDirectoryStripMenuItem.Click += new System.EventHandler(this.selectDirectoryStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(209, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.openToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openToolStripMenuItem.Text = "&Open Assembly";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openInjectionAssemblyToolStripMenuItem
            // 
            this.openInjectionAssemblyToolStripMenuItem.Name = "openInjectionAssemblyToolStripMenuItem";
            this.openInjectionAssemblyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.openInjectionAssemblyToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openInjectionAssemblyToolStripMenuItem.Text = "Open &Injection Assembly";
            this.openInjectionAssemblyToolStripMenuItem.Click += new System.EventHandler(this.openInjectionAssemblyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(209, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // projectMenu
            // 
            this.projectMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem});
            this.projectMenu.Name = "projectMenu";
            this.projectMenu.Size = new System.Drawing.Size(53, 20);
            this.projectMenu.Text = "&Project";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.openProjectToolStripMenuItem.Text = "&Open";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveProjectToolStripMenuItem.Text = "&Save";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // injectToolStripMenuItem
            // 
            this.injectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem});
            this.injectToolStripMenuItem.Name = "injectToolStripMenuItem";
            this.injectToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.injectToolStripMenuItem.Text = "&Inject";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.runToolStripMenuItem.Text = "&Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.resetToolStripMenuItem.Text = "Rese&t";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aboutToolStripMenuItem.Text = "Abou&t";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openAssemblyDialog
            // 
            this.openAssemblyDialog.Filter = "Executable files (*.exe)|*.exe|Assembly files (*.dll)|*.dll";
            this.openAssemblyDialog.Multiselect = true;
            // 
            // openInjectionDialog
            // 
            this.openInjectionDialog.Filter = "Assembly files (*.dll)|*.dll|Executable files (*.exe)|*.exe";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tblMain);
            this.splitContainer1.Size = new System.Drawing.Size(745, 484);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.treeTarget, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeInjectionCode, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(220, 484);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // treeTarget
            // 
            this.treeTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.treeTarget.CheckBoxes = true;
            this.treeTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTarget.FullRowSelect = true;
            this.treeTarget.Location = new System.Drawing.Point(3, 3);
            this.treeTarget.Name = "treeTarget";
            this.treeTarget.Size = new System.Drawing.Size(214, 236);
            this.treeTarget.TabIndex = 0;
            this.treeTarget.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeTarget_AfterCheck);
            // 
            // treeInjectionCode
            // 
            this.treeInjectionCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.treeInjectionCode.CheckBoxes = true;
            this.treeInjectionCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeInjectionCode.FullRowSelect = true;
            this.treeInjectionCode.Location = new System.Drawing.Point(3, 245);
            this.treeInjectionCode.Name = "treeInjectionCode";
            this.treeInjectionCode.Size = new System.Drawing.Size(214, 236);
            this.treeInjectionCode.TabIndex = 1;
            this.treeInjectionCode.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeInjectionCode_AfterCheck);
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 183F));
            this.tblMain.Controls.Add(this.lstOutput, 0, 2);
            this.tblMain.Controls.Add(this.grdCombination, 0, 0);
            this.tblMain.Controls.Add(this.btnAdd, 0, 1);
            this.tblMain.Controls.Add(this.btnRemove, 1, 1);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 3;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.15929F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.84071F));
            this.tblMain.Size = new System.Drawing.Size(521, 484);
            this.tblMain.TabIndex = 0;
            // 
            // lstOutput
            // 
            this.tblMain.SetColumnSpan(this.lstOutput, 2);
            this.lstOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOutput.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(0, 324);
            this.lstOutput.Margin = new System.Windows.Forms.Padding(0);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(521, 160);
            this.lstOutput.TabIndex = 0;
            this.lstOutput.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstOutput_DrawItem);
            // 
            // grdCombination
            // 
            this.grdCombination.AllowUserToAddRows = false;
            this.grdCombination.AllowUserToDeleteRows = false;
            this.grdCombination.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grdCombination.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdCombination.AutoGenerateColumns = false;
            this.grdCombination.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdCombination.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.grdCombination.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdCombination.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCombination.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.assemblyNameDataGridViewTextBoxColumn,
            this.methodNameDataGridViewTextBoxColumn,
            this.injectorNameDataGridViewTextBoxColumn});
            this.tblMain.SetColumnSpan(this.grdCombination, 2);
            this.grdCombination.DataSource = this.bindingSource1;
            this.grdCombination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCombination.Location = new System.Drawing.Point(3, 3);
            this.grdCombination.Name = "grdCombination";
            this.grdCombination.ReadOnly = true;
            this.grdCombination.RowHeadersVisible = false;
            this.grdCombination.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdCombination.Size = new System.Drawing.Size(515, 278);
            this.grdCombination.TabIndex = 1;
            // 
            // assemblyNameDataGridViewTextBoxColumn
            // 
            this.assemblyNameDataGridViewTextBoxColumn.DataPropertyName = "AssemblyName";
            this.assemblyNameDataGridViewTextBoxColumn.HeaderText = "Assembly";
            this.assemblyNameDataGridViewTextBoxColumn.Name = "assemblyNameDataGridViewTextBoxColumn";
            this.assemblyNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // methodNameDataGridViewTextBoxColumn
            // 
            this.methodNameDataGridViewTextBoxColumn.DataPropertyName = "MethodName";
            this.methodNameDataGridViewTextBoxColumn.HeaderText = "Method";
            this.methodNameDataGridViewTextBoxColumn.Name = "methodNameDataGridViewTextBoxColumn";
            this.methodNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // injectorNameDataGridViewTextBoxColumn
            // 
            this.injectorNameDataGridViewTextBoxColumn.DataPropertyName = "InjectorName";
            this.injectorNameDataGridViewTextBoxColumn.HeaderText = "Injector";
            this.injectorNameDataGridViewTextBoxColumn.Name = "injectorNameDataGridViewTextBoxColumn";
            this.injectorNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(CInject.Data.InjectionMapping);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(3, 287);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(332, 34);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "&Add selected injectors to selected methods intarget assembly";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(341, 287);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(177, 34);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "&Remove selected row";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // openProjectFileDialog
            // 
            this.openProjectFileDialog.Filter = "Project files (*.ciproj)|*.ciproj";
            // 
            // saveProjectFileDialog
            // 
            this.saveProjectFileDialog.DefaultExt = "ciproj";
            this.saveProjectFileDialog.Filter = "Project files (*.ciproj)|*.ciproj";
            this.saveProjectFileDialog.InitialDirectory = "C:\\";
            this.saveProjectFileDialog.Title = "Save CInject Project File";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 508);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmMain";
            this.Text = "CInject by Punit Ganshani";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCombination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openAssemblyDialog;
        private System.Windows.Forms.ToolStripMenuItem openInjectionAssemblyToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openInjectionDialog;
        private System.Windows.Forms.ToolStripMenuItem injectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CInjectTreeView treeTarget;
        private CInjectTreeView treeInjectionCode;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.DataGridView grdCombination;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn assemblyNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn methodNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn injectorNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ToolStripMenuItem selectDirectoryStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openProjectFileDialog;
        private System.Windows.Forms.ToolStripMenuItem projectMenu;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveProjectFileDialog;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
    }
}

