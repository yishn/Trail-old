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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Documents");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Bookmarks", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("System");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Data");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("DVD");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Drives", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            this.pnlSplit = new System.Windows.Forms.SplitContainer();
            this.ilSide = new System.Windows.Forms.ImageList(this.components);
            this.ilItems = new System.Windows.Forms.ImageList(this.components);
            this.tvSide = new Trail.Controls.TreeViewModern();
            this.cvColumns = new Trail.Controls.ColumnView();
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
            this.pnlSplit.Panel1.Controls.Add(this.tvSide);
            // 
            // pnlSplit.Panel2
            // 
            this.pnlSplit.Panel2.Controls.Add(this.cvColumns);
            this.pnlSplit.Size = new System.Drawing.Size(842, 382);
            this.pnlSplit.SplitterDistance = 196;
            this.pnlSplit.SplitterWidth = 2;
            this.pnlSplit.TabIndex = 1;
            this.pnlSplit.TabStop = false;
            // 
            // ilSide
            // 
            this.ilSide.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSide.ImageStream")));
            this.ilSide.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSide.Images.SetKeyName(0, "folder");
            this.ilSide.Images.SetKeyName(1, "star");
            this.ilSide.Images.SetKeyName(2, "bookmark");
            this.ilSide.Images.SetKeyName(3, "computer");
            this.ilSide.Images.SetKeyName(4, "drive");
            this.ilSide.Images.SetKeyName(5, "disc");
            this.ilSide.Images.SetKeyName(6, "network");
            this.ilSide.Images.SetKeyName(7, "removable");
            this.ilSide.Images.SetKeyName(8, "unknown");
            // 
            // ilItems
            // 
            this.ilItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilItems.ImageStream")));
            this.ilItems.TransparentColor = System.Drawing.Color.Transparent;
            this.ilItems.Images.SetKeyName(0, ".folder");
            this.ilItems.Images.SetKeyName(1, ".file");
            // 
            // tvSide
            // 
            this.tvSide.BackColor = System.Drawing.SystemColors.Control;
            this.tvSide.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSide.FullRowSelect = true;
            this.tvSide.HideSelection = false;
            this.tvSide.HotTracking = true;
            this.tvSide.ImageIndex = 0;
            this.tvSide.ImageList = this.ilSide;
            this.tvSide.Indent = 19;
            this.tvSide.ItemHeight = 22;
            this.tvSide.Location = new System.Drawing.Point(0, 0);
            this.tvSide.Name = "tvSide";
            treeNode1.Name = "Knoten0";
            treeNode1.Text = "Documents";
            treeNode2.ImageKey = "bookmark";
            treeNode2.Name = "nodeBookmarks";
            treeNode2.SelectedImageKey = "bookmark";
            treeNode2.Text = "Bookmarks";
            treeNode3.ImageKey = "drive";
            treeNode3.Name = "Knoten0";
            treeNode3.SelectedImageKey = "drive";
            treeNode3.Text = "System";
            treeNode4.ImageKey = "drive";
            treeNode4.Name = "Knoten2";
            treeNode4.SelectedImageKey = "drive";
            treeNode4.Text = "Data";
            treeNode5.ImageKey = "disc";
            treeNode5.Name = "Knoten4";
            treeNode5.SelectedImageKey = "disc";
            treeNode5.Text = "DVD";
            treeNode6.ImageKey = "computer";
            treeNode6.Name = "nodeDrives";
            treeNode6.SelectedImageKey = "computer";
            treeNode6.Text = "Drives";
            this.tvSide.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode6});
            this.tvSide.SelectedImageIndex = 0;
            this.tvSide.ShowLines = false;
            this.tvSide.ShowNodeToolTips = true;
            this.tvSide.Size = new System.Drawing.Size(196, 382);
            this.tvSide.TabIndex = 0;
            // 
            // cvColumns
            // 
            this.cvColumns.AutoScroll = true;
            this.cvColumns.BackColor = System.Drawing.Color.White;
            this.cvColumns.DefaultColumnWidth = 200;
            this.cvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cvColumns.ImageList = this.ilItems;
            this.cvColumns.Location = new System.Drawing.Point(0, 0);
            this.cvColumns.Name = "cvColumns";
            this.cvColumns.Size = new System.Drawing.Size(644, 382);
            this.cvColumns.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 382);
            this.Controls.Add(this.pnlSplit);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Trail";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSplit)).EndInit();
            this.pnlSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer pnlSplit;
        private Trail.Controls.TreeViewModern tvSide;
        private Controls.ColumnView cvColumns;
        private System.Windows.Forms.ImageList ilSide;
        private System.Windows.Forms.ImageList ilItems;



    }
}

