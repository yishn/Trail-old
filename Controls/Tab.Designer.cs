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
            this.btnClose = new Trail.Controls.FlatButton();
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
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(153, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // Tab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblText);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(175, 231);
            this.Name = "Tab";
            this.Size = new System.Drawing.Size(175, 20);
            this.MouseEnter += new System.EventHandler(this.Tab_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Tab_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private FlatButton btnClose;
    }
}
