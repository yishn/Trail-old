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
            this.errorPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorMessageLabel = new System.Windows.Forms.Label();
            this.ListViewControl = new Trail.Controls.ListViewModern();
            this.headerColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorPanel
            // 
            this.errorPanel.Controls.Add(this.errorMessageLabel);
            this.errorPanel.Controls.Add(this.label2);
            this.errorPanel.Controls.Add(this.label1);
            this.errorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorPanel.Location = new System.Drawing.Point(0, 0);
            this.errorPanel.Name = "errorPanel";
            this.errorPanel.Size = new System.Drawing.Size(200, 381);
            this.errorPanel.TabIndex = 2;
            this.errorPanel.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(78, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Error";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(63, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = "";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseCompatibleTextRendering = true;
            // 
            // errorMessageLabel
            // 
            this.errorMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.errorMessageLabel.ForeColor = System.Drawing.Color.Gray;
            this.errorMessageLabel.Location = new System.Drawing.Point(12, 216);
            this.errorMessageLabel.Name = "errorMessageLabel";
            this.errorMessageLabel.Size = new System.Drawing.Size(176, 47);
            this.errorMessageLabel.TabIndex = 2;
            this.errorMessageLabel.Text = "Error Message";
            this.errorMessageLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ListViewControl
            // 
            this.ListViewControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListViewControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerColumn});
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
            // headerColumn
            // 
            this.headerColumn.Text = "Header";
            this.headerColumn.Width = 150;
            // 
            // ColumnControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.errorPanel);
            this.Controls.Add(this.ListViewControl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ColumnControl";
            this.Size = new System.Drawing.Size(200, 381);
            this.errorPanel.ResumeLayout(false);
            this.errorPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader headerColumn;
        public ListViewModern ListViewControl;
        private System.Windows.Forms.Panel errorPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label errorMessageLabel;
    }
}
