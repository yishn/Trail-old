using Trail.Controls;
namespace Trail.Actions {
    partial class ActionControl {
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
            this.headerLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.cancelButton = new Trail.Controls.FlatButton();
            this.progressBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Controls.Add(this.progressBarValue);
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 45);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 5);
            this.progressBar.TabIndex = 0;
            this.progressBar.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.progressBar.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // progressBarValue
            // 
            this.progressBarValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.progressBarValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.progressBarValue.Location = new System.Drawing.Point(0, 0);
            this.progressBarValue.Name = "progressBarValue";
            this.progressBarValue.Size = new System.Drawing.Size(0, 5);
            this.progressBarValue.TabIndex = 0;
            this.progressBarValue.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.progressBarValue.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // headerLabel
            // 
            this.headerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerLabel.AutoEllipsis = true;
            this.headerLabel.Location = new System.Drawing.Point(3, 8);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(171, 15);
            this.headerLabel.TabIndex = 1;
            this.headerLabel.Text = "ActionProgressControl";
            this.headerLabel.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.headerLabel.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.AutoEllipsis = true;
            this.descriptionLabel.ForeColor = System.Drawing.Color.Gray;
            this.descriptionLabel.Location = new System.Drawing.Point(3, 23);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(194, 18);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "Description";
            this.descriptionLabel.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.descriptionLabel.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(67)))), ((int)(((byte)(67)))));
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(180, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(20, 20);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            this.cancelButton.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.cancelButton.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // ActionProgressControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.progressBar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.Name = "ActionProgressControl";
            this.Size = new System.Drawing.Size(200, 50);
            this.SizeChanged += new System.EventHandler(this.ActionProgressControl_SizeChanged);
            this.MouseEnter += new System.EventHandler(this.ActionProgressControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ActionProgressControl_MouseLeave);
            this.progressBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel progressBar;
        private System.Windows.Forms.Panel progressBarValue;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private FlatButton cancelButton;
    }
}
