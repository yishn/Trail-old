namespace Trail {
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.sidebarImages = new System.Windows.Forms.ImageList(this.components);
            this.itemsImages = new System.Windows.Forms.ImageList(this.components);
            this.sidebar = new Trail.Modules.Sidebar();
            this.tabBar = new Trail.Modules.NavigatingTabBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.sidebar);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer.Panel2.Controls.Add(this.tabBar);
            this.splitContainer.Size = new System.Drawing.Size(842, 382);
            this.splitContainer.SplitterDistance = 196;
            this.splitContainer.SplitterWidth = 2;
            this.splitContainer.TabIndex = 1;
            this.splitContainer.TabStop = false;
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
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.SystemColors.Control;
            this.sidebar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidebar.FullRowSelect = true;
            this.sidebar.HideSelection = false;
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
            this.sidebar.Size = new System.Drawing.Size(196, 382);
            this.sidebar.TabIndex = 0;
            this.sidebar.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.sidebar_AfterSelect);
            // 
            // tabBar
            // 
            this.tabBar.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.tabBar.AllowNoTabs = false;
            this.tabBar.CurrentTab = null;
            this.tabBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabBar.Location = new System.Drawing.Point(0, 0);
            this.tabBar.Name = "tabBar";
            this.tabBar.ShowNewTabButton = true;
            this.tabBar.Size = new System.Drawing.Size(644, 22);
            this.tabBar.TabIndex = 2;
            this.tabBar.AddButtonClicked += new System.EventHandler(this.tabBar_AddButtonClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 382);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Trail";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ImageList sidebarImages;
        private System.Windows.Forms.ImageList itemsImages;
        private Modules.Sidebar sidebar;
        private Trail.Modules.NavigatingTabBar tabBar;
    }
}

