namespace Trail.Controls {
    partial class ActionProgressControl {
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
            this.progressBar = new System.Windows.Forms.Panel();
            this.progressBarValue = new System.Windows.Forms.Panel();
            this.progressBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Controls.Add(this.progressBarValue);
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 39);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 5);
            this.progressBar.TabIndex = 0;
            // 
            // progressBarValue
            // 
            this.progressBarValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.progressBarValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.progressBarValue.Location = new System.Drawing.Point(0, 0);
            this.progressBarValue.Name = "progressBarValue";
            this.progressBarValue.Size = new System.Drawing.Size(150, 5);
            this.progressBarValue.TabIndex = 0;
            // 
            // ActionProgressControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.Controls.Add(this.progressBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.Name = "ActionProgressControl";
            this.Size = new System.Drawing.Size(200, 44);
            this.progressBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel progressBar;
        private System.Windows.Forms.Panel progressBarValue;
    }
}
