namespace Trail.Controls {
    partial class HelperForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.controlBox = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.controlBoxLabel = new System.Windows.Forms.Label();
            this.controlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlBox
            // 
            this.controlBox.Controls.Add(this.closeButton);
            this.controlBox.Controls.Add(this.controlBoxLabel);
            this.controlBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlBox.Location = new System.Drawing.Point(0, 0);
            this.controlBox.Name = "controlBox";
            this.controlBox.Size = new System.Drawing.Size(391, 27);
            this.controlBox.TabIndex = 0;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(67)))), ((int)(((byte)(67)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(346, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(45, 20);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // controlBoxLabel
            // 
            this.controlBoxLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlBoxLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlBoxLabel.Location = new System.Drawing.Point(0, 0);
            this.controlBoxLabel.Name = "controlBoxLabel";
            this.controlBoxLabel.Size = new System.Drawing.Size(391, 27);
            this.controlBoxLabel.TabIndex = 1;
            this.controlBoxLabel.Text = "HelperForm";
            this.controlBoxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.controlBoxLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.controlBoxLabel_MouseDown);
            this.controlBoxLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.controlBoxLabel_MouseMove);
            // 
            // HelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(391, 276);
            this.ControlBox = false;
            this.Controls.Add(this.controlBox);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelperForm";
            this.Opacity = 0.98D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HelperForm_FormClosing);
            this.Load += new System.EventHandler(this.HelperForm_Load);
            this.Shown += new System.EventHandler(this.HelperForm_Shown);
            this.controlBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label controlBoxLabel;
    }
}