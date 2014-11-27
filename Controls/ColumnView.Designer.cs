namespace Trail.Controls {
    partial class ColumnView {
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
            this.pnlColumns = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlColumns
            // 
            this.pnlColumns.AutoScroll = true;
            this.pnlColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColumns.Location = new System.Drawing.Point(0, 0);
            this.pnlColumns.Name = "pnlColumns";
            this.pnlColumns.Size = new System.Drawing.Size(871, 388);
            this.pnlColumns.TabIndex = 0;
            // 
            // ColumnView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlColumns);
            this.Name = "ColumnView";
            this.Size = new System.Drawing.Size(871, 388);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlColumns;
    }
}
