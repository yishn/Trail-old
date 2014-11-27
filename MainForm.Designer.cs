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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Bookmarks");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Drives");
            this.pnlSplit = new System.Windows.Forms.SplitContainer();
            this.ilSide = new System.Windows.Forms.ImageList(this.components);
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
            this.pnlSplit.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.pnlSplit.Size = new System.Drawing.Size(932, 382);
            this.pnlSplit.SplitterDistance = 218;
            this.pnlSplit.TabIndex = 1;
            // 
            // ilSide
            // 
            this.ilSide.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSide.ImageStream")));
            this.ilSide.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSide.Images.SetKeyName(0, "folder");
            this.ilSide.Images.SetKeyName(1, "bookmark");
            this.ilSide.Images.SetKeyName(2, "drive");
            this.ilSide.Images.SetKeyName(3, "computer");
            this.ilSide.Images.SetKeyName(4, "star");
            // 
            // tvSide
            // 
            this.tvSide.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSide.FullRowSelect = true;
            this.tvSide.HideSelection = false;
            this.tvSide.ImageIndex = 0;
            this.tvSide.ImageList = this.ilSide;
            this.tvSide.Indent = 19;
            this.tvSide.ItemHeight = 25;
            this.tvSide.Location = new System.Drawing.Point(0, 0);
            this.tvSide.Name = "tvSide";
            treeNode1.ImageKey = "bookmark";
            treeNode1.Name = "nodeBookmarks";
            treeNode1.SelectedImageKey = "bookmark";
            treeNode1.Text = "Bookmarks";
            treeNode2.ImageKey = "computer";
            treeNode2.Name = "nodeDrives";
            treeNode2.SelectedImageKey = "computer";
            treeNode2.Text = "Drives";
            this.tvSide.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.tvSide.SelectedImageIndex = 0;
            this.tvSide.ShowLines = false;
            this.tvSide.ShowNodeToolTips = true;
            this.tvSide.Size = new System.Drawing.Size(218, 382);
            this.tvSide.TabIndex = 0;
            // 
            // cvColumns
            // 
            this.cvColumns.DefaultColumnWidth = 200;
            this.cvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cvColumns.Location = new System.Drawing.Point(0, 0);
            this.cvColumns.Name = "cvColumns";
            this.cvColumns.Size = new System.Drawing.Size(710, 382);
            this.cvColumns.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 382);
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



    }
}

