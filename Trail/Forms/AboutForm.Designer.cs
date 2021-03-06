﻿namespace Trail.Forms {
    partial class AboutForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.logo = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.LinkLabel();
            this.labelGitHub = new System.Windows.Forms.LinkLabel();
            this.labelIcons = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(21, 26);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(128, 128);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.logo.TabIndex = 1;
            this.logo.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI Light", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(155, 26);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(122, 74);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Trail";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(168, 104);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(304, 21);
            this.labelDescription.TabIndex = 3;
            this.labelDescription.Text = "An extendable column view file manager for Windows. ";
            this.labelDescription.UseCompatibleTextRendering = true;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Segoe UI Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.Gray;
            this.labelVersion.Location = new System.Drawing.Point(266, 56);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(39, 37);
            this.labelVersion.TabIndex = 4;
            this.labelVersion.Text = "v1";
            // 
            // labelCopyright
            // 
            this.labelCopyright.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.LinkArea = new System.Windows.Forms.LinkArea(12, 12);
            this.labelCopyright.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.labelCopyright.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelCopyright.Location = new System.Drawing.Point(168, 125);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(179, 21);
            this.labelCopyright.TabIndex = 5;
            this.labelCopyright.TabStop = true;
            this.labelCopyright.Text = "Copyright © Yichuan Shen 2014";
            this.labelCopyright.UseCompatibleTextRendering = true;
            this.labelCopyright.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelCopyright.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelCopyright_LinkClicked);
            // 
            // labelGitHub
            // 
            this.labelGitHub.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelGitHub.AutoSize = true;
            this.labelGitHub.ForeColor = System.Drawing.Color.Gray;
            this.labelGitHub.LinkArea = new System.Windows.Forms.LinkArea(15, 6);
            this.labelGitHub.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.labelGitHub.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelGitHub.Location = new System.Drawing.Point(348, 179);
            this.labelGitHub.Name = "labelGitHub";
            this.labelGitHub.Size = new System.Drawing.Size(132, 21);
            this.labelGitHub.TabIndex = 6;
            this.labelGitHub.TabStop = true;
            this.labelGitHub.Text = "Source code on GitHub";
            this.labelGitHub.UseCompatibleTextRendering = true;
            this.labelGitHub.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelGitHub_LinkClicked);
            // 
            // labelIcons
            // 
            this.labelIcons.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelIcons.AutoSize = true;
            this.labelIcons.ForeColor = System.Drawing.Color.Gray;
            this.labelIcons.LinkArea = new System.Windows.Forms.LinkArea(15, 17);
            this.labelIcons.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.labelIcons.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelIcons.Location = new System.Drawing.Point(21, 179);
            this.labelIcons.Name = "labelIcons";
            this.labelIcons.Size = new System.Drawing.Size(202, 21);
            this.labelIcons.TabIndex = 7;
            this.labelIcons.TabStop = true;
            this.labelIcons.Text = "Fugue icons by Yusuke Kamiyamane";
            this.labelIcons.UseCompatibleTextRendering = true;
            this.labelIcons.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelIcons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelIcons_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(494, 211);
            this.Controls.Add(this.labelIcons);
            this.Controls.Add(this.labelGitHub);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.logo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Trail";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.LinkLabel labelCopyright;
        private System.Windows.Forms.LinkLabel labelGitHub;
        private System.Windows.Forms.LinkLabel labelIcons;
    }
}