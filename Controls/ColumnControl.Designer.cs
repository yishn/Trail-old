namespace Trail.Controls {
    partial class ColumnControl {
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.ListViewControl = new Trail.Controls.ListViewModern();
            this.colHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // ListViewControl
            // 
            this.ListViewControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListViewControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeader});
            this.ListViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewControl.FullRowSelect = true;
            this.ListViewControl.HideSelection = false;
            this.ListViewControl.Location = new System.Drawing.Point(0, 0);
            this.ListViewControl.Name = "ListViewControl";
            this.ListViewControl.ShowItemToolTips = true;
            this.ListViewControl.Size = new System.Drawing.Size(200, 381);
            this.ListViewControl.TabIndex = 0;
            this.ListViewControl.UseCompatibleStateImageBehavior = false;
            this.ListViewControl.View = System.Windows.Forms.View.Details;
            this.ListViewControl.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListViewControl_ColumnWidthChanging);
            this.ListViewControl.ClientSizeChanged += new System.EventHandler(this.ListViewControl_ClientSizeChanged);
            // 
            // colHeader
            // 
            this.colHeader.Text = "Header";
            this.colHeader.Width = 150;
            // 
            // ColumnControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ListViewControl);
            this.Name = "ColumnControl";
            this.Size = new System.Drawing.Size(200, 381);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader colHeader;
        public ListViewModern ListViewControl;
    }
}
