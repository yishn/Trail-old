﻿namespace Trail {
    partial class MainForm {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sidebarImages = new System.Windows.Forms.ImageList(this.components);
            this.itemsImages = new System.Windows.Forms.ImageList(this.components);
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.trailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutTrailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreClosedTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.nextTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.goToPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageAppsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new Trail.Controls.SplitContainerModern();
            this.sidebar = new Trail.Modules.Sidebar();
            this.tabBar = new Trail.Modules.NavigatingTabBar();
            this.iconQueue = new Trail.Modules.ItemsIconQueue();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarImages
            // 
            this.sidebarImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("sidebarImages.ImageStream")));
            this.sidebarImages.TransparentColor = System.Drawing.Color.Transparent;
            this.sidebarImages.Images.SetKeyName(0, "folder");
            this.sidebarImages.Images.SetKeyName(1, "bookmarks");
            this.sidebarImages.Images.SetKeyName(2, "drives");
            this.sidebarImages.Images.SetKeyName(3, "drive");
            this.sidebarImages.Images.SetKeyName(4, "disc");
            this.sidebarImages.Images.SetKeyName(5, "network");
            this.sidebarImages.Images.SetKeyName(6, "removable");
            this.sidebarImages.Images.SetKeyName(7, "unknown");
            this.sidebarImages.Images.SetKeyName(8, "favorites");
            // 
            // itemsImages
            // 
            this.itemsImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("itemsImages.ImageStream")));
            this.itemsImages.TransparentColor = System.Drawing.Color.Transparent;
            this.itemsImages.Images.SetKeyName(0, ".folder");
            this.itemsImages.Images.SetKeyName(1, ".file");
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trailToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.extrasToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(2);
            this.mainMenu.Size = new System.Drawing.Size(826, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // trailToolStripMenuItem
            // 
            this.trailToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.aboutTrailToolStripMenuItem});
            this.trailToolStripMenuItem.Name = "trailToolStripMenuItem";
            this.trailToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.trailToolStripMenuItem.Text = "&Trail";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+,";
            this.preferencesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemcomma)));
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.preferencesToolStripMenuItem.Text = "Browse &Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(210, 6);
            // 
            // aboutTrailToolStripMenuItem
            // 
            this.aboutTrailToolStripMenuItem.Name = "aboutTrailToolStripMenuItem";
            this.aboutTrailToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.aboutTrailToolStripMenuItem.Text = "&About Trail";
            this.aboutTrailToolStripMenuItem.Click += new System.EventHandler(this.aboutTrailToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTabToolStripMenuItem,
            this.duplicateTabToolStripMenuItem,
            this.closeTabToolStripMenuItem,
            this.restoreClosedTabToolStripMenuItem,
            this.toolStripMenuItem3,
            this.nextTabToolStripMenuItem,
            this.previousTabToolStripMenuItem,
            this.toolStripMenuItem1,
            this.goToPathToolStripMenuItem});
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.historyToolStripMenuItem.Text = "&Navigation";
            // 
            // newTabToolStripMenuItem
            // 
            this.newTabToolStripMenuItem.Name = "newTabToolStripMenuItem";
            this.newTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.newTabToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.newTabToolStripMenuItem.Text = "New &Tab";
            this.newTabToolStripMenuItem.Click += new System.EventHandler(this.newTabToolStripMenuItem_Click);
            // 
            // duplicateTabToolStripMenuItem
            // 
            this.duplicateTabToolStripMenuItem.Name = "duplicateTabToolStripMenuItem";
            this.duplicateTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.duplicateTabToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.duplicateTabToolStripMenuItem.Text = "&Duplicate Tab";
            this.duplicateTabToolStripMenuItem.Click += new System.EventHandler(this.duplicateTabToolStripMenuItem_Click);
            // 
            // closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.closeTabToolStripMenuItem.Text = "&Close Tab";
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.closeTabToolStripMenuItem_Click);
            // 
            // restoreClosedTabToolStripMenuItem
            // 
            this.restoreClosedTabToolStripMenuItem.Name = "restoreClosedTabToolStripMenuItem";
            this.restoreClosedTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.restoreClosedTabToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.restoreClosedTabToolStripMenuItem.Text = "&Restore Closed Tab";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(298, 6);
            // 
            // nextTabToolStripMenuItem
            // 
            this.nextTabToolStripMenuItem.Name = "nextTabToolStripMenuItem";
            this.nextTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)));
            this.nextTabToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.nextTabToolStripMenuItem.Text = "&Next Tab";
            this.nextTabToolStripMenuItem.Click += new System.EventHandler(this.nextTabToolStripMenuItem_Click);
            // 
            // previousTabToolStripMenuItem
            // 
            this.previousTabToolStripMenuItem.Name = "previousTabToolStripMenuItem";
            this.previousTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Tab)));
            this.previousTabToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.previousTabToolStripMenuItem.Text = "&Previous Tab";
            this.previousTabToolStripMenuItem.Click += new System.EventHandler(this.previousTabToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(298, 6);
            // 
            // goToPathToolStripMenuItem
            // 
            this.goToPathToolStripMenuItem.Name = "goToPathToolStripMenuItem";
            this.goToPathToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.goToPathToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
            this.goToPathToolStripMenuItem.Text = "&Goto Path...";
            this.goToPathToolStripMenuItem.Click += new System.EventHandler(this.goToPathToolStripMenuItem_Click);
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageAppsToolStripMenuItem,
            this.packagesToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.extrasToolStripMenuItem.Text = "&Extras";
            // 
            // manageAppsToolStripMenuItem
            // 
            this.manageAppsToolStripMenuItem.Name = "manageAppsToolStripMenuItem";
            this.manageAppsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.manageAppsToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.manageAppsToolStripMenuItem.Text = "Manage &Apps...";
            // 
            // packagesToolStripMenuItem
            // 
            this.packagesToolStripMenuItem.Name = "packagesToolStripMenuItem";
            this.packagesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.packagesToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.packagesToolStripMenuItem.Text = "&Packages";
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer.Panel1.Controls.Add(this.sidebar);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer.Panel2.Controls.Add(this.tabBar);
            this.splitContainer.Size = new System.Drawing.Size(826, 359);
            this.splitContainer.SplitterDistance = 196;
            this.splitContainer.SplitterWidth = 2;
            this.splitContainer.TabIndex = 1;
            this.splitContainer.TabStop = false;
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.SystemColors.Control;
            this.sidebar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidebar.FullRowSelect = true;
            this.sidebar.HotTracking = true;
            this.sidebar.ImageIndex = 0;
            this.sidebar.ImageList = this.sidebarImages;
            this.sidebar.Indent = 19;
            this.sidebar.ItemHeight = 22;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.SelectedImageIndex = 0;
            this.sidebar.ShowLines = false;
            this.sidebar.ShowNodeToolTips = true;
            this.sidebar.Size = new System.Drawing.Size(196, 359);
            this.sidebar.TabIndex = 0;
            this.sidebar.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.sidebar_AfterSelect);
            // 
            // tabBar
            // 
            this.tabBar.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.tabBar.AllowNoTabs = false;
            this.tabBar.CurrentTab = null;
            this.tabBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabBar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabBar.Location = new System.Drawing.Point(0, 0);
            this.tabBar.Name = "tabBar";
            this.tabBar.ShowNewTabButton = true;
            this.tabBar.Size = new System.Drawing.Size(628, 22);
            this.tabBar.TabIndex = 2;
            this.tabBar.AddButtonClicked += new System.EventHandler(this.tabBar_AddButtonClicked);
            // 
            // iconQueue
            // 
            this.iconQueue.ImageList = this.itemsImages;
            this.iconQueue.WorkerReportsProgress = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 383);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "Trail";
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Trail.Controls.SplitContainerModern splitContainer;
        private System.Windows.Forms.ImageList sidebarImages;
        private System.Windows.Forms.ImageList itemsImages;
        private Modules.Sidebar sidebar;
        private Trail.Modules.NavigatingTabBar tabBar;
        private Modules.ItemsIconQueue iconQueue;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem trailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutTrailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageAppsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem newTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreClosedTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem goToPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem duplicateTabToolStripMenuItem;
    }
}

