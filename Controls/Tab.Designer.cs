namespace Trail.Controls {
    partial class Tab {
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
            this.lblText = new System.Windows.Forms.Label();
            this.closeButton = new Trail.Controls.FlatButton();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.AutoEllipsis = true;
            this.lblText.BackColor = System.Drawing.Color.Transparent;
            this.lblText.Location = new System.Drawing.Point(3, 3);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(55, 15);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "TabText";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblText.Click += new System.EventHandler(this.lblText_Click);
            this.lblText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblText_MouseClick);
            this.lblText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblText_MouseDown);
            this.lblText.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.lblText.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            this.lblText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblText_MouseMove);
            this.lblText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblText_MouseUp);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(153, 0);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(20, 20);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Visible = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.closeButton.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.closeButton.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // Tab
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.lblText);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(175, 231);
            this.Name = "Tab";
            this.Size = new System.Drawing.Size(175, 20);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Tab_DragEnter);
            this.DragLeave += new System.EventHandler(this.Tab_DragLeave);
            this.MouseEnter += new System.EventHandler(this.Tab_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Tab_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private FlatButton closeButton;
    }
}
