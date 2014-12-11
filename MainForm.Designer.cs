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
            this.pnlSplit = new System.Windows.Forms.SplitContainer();
            this.tvSidebar = new Trail.Modules.Sidebar();
            this.ilSide = new System.Windows.Forms.ImageList(this.components);
            this.tabBar1 = new Trail.Modules.NavigatingTabBar();
            this.ilItems = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSplit)).BeginInit();
            this.pnlSplit.Panel1.SuspendLayout();
            this.pnlSplit.Panel2.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSplit
            // 
            this.pnlSplit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.pnlSplit.Location = new System.Drawing.Point(0, 0);
            this.pnlSplit.Name = "pnlSplit";
            // 
            // pnlSplit.Panel1
            // 
            this.pnlSplit.Panel1.Controls.Add(this.tvSidebar);
            // 
            // pnlSplit.Panel2
            // 
            this.pnlSplit.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSplit.Panel2.Controls.Add(this.tabBar1);
            this.pnlSplit.Size = new System.Drawing.Size(842, 382);
            this.pnlSplit.SplitterDistance = 196;
            this.pnlSplit.SplitterWidth = 2;
            this.pnlSplit.TabIndex = 1;
            this.pnlSplit.TabStop = false;
            // 
            // tvSidebar
            // 
            this.tvSidebar.BackColor = System.Drawing.SystemColors.Control;
            this.tvSidebar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSidebar.FullRowSelect = true;
            this.tvSidebar.HideSelection = false;
            this.tvSidebar.HotTracking = true;
            this.tvSidebar.ImageIndex = 0;
            this.tvSidebar.ImageList = this.ilSide;
            this.tvSidebar.Indent = 19;
            this.tvSidebar.ItemHeight = 22;
            this.tvSidebar.Location = new System.Drawing.Point(0, 0);
            this.tvSidebar.Name = "tvSidebar";
            this.tvSidebar.SelectedImageIndex = 0;
            this.tvSidebar.ShowLines = false;
            this.tvSidebar.ShowNodeToolTips = true;
            this.tvSidebar.Size = new System.Drawing.Size(196, 382);
            this.tvSidebar.TabIndex = 0;
            this.tvSidebar.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSidebar_AfterSelect);
            // 
            // ilSide
            // 
            this.ilSide.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSide.ImageStream")));
            this.ilSide.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSide.Images.SetKeyName(0, "folder");
            this.ilSide.Images.SetKeyName(1, "bookmarks");
            this.ilSide.Images.SetKeyName(2, "drives");
            this.ilSide.Images.SetKeyName(3, "drive");
            this.ilSide.Images.SetKeyName(4, "disc");
            this.ilSide.Images.SetKeyName(5, "network");
            this.ilSide.Images.SetKeyName(6, "removable");
            this.ilSide.Images.SetKeyName(7, "unknown");
            this.ilSide.Images.SetKeyName(8, "favorites");
            // 
            // tabBar1
            // 
            this.tabBar1.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.tabBar1.AllowNoTabs = false;
            this.tabBar1.CurrentTab = null;
            this.tabBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabBar1.Location = new System.Drawing.Point(0, 0);
            this.tabBar1.Name = "tabBar1";
            this.tabBar1.ShowNewTabButton = true;
            this.tabBar1.Size = new System.Drawing.Size(644, 22);
            this.tabBar1.TabIndex = 2;
            this.tabBar1.AddButtonClicked += new System.EventHandler(this.tabBar1_AddButtonClicked);
            // 
            // ilItems
            // 
            this.ilItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilItems.ImageStream")));
            this.ilItems.TransparentColor = System.Drawing.Color.Transparent;
            this.ilItems.Images.SetKeyName(0, ".folder");
            this.ilItems.Images.SetKeyName(1, ".file");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 382);
            this.Controls.Add(this.pnlSplit);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Trail";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSplit)).EndInit();
            this.pnlSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer pnlSplit;
        private System.Windows.Forms.ImageList ilSide;
        private System.Windows.Forms.ImageList ilItems;
        private Modules.Sidebar tvSidebar;
        private Trail.Modules.NavigatingTabBar tabBar1;
    }
}

