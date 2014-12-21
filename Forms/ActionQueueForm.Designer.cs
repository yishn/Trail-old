namespace Trail.Forms {
    partial class ActionQueueForm {
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
            this.actionsList = new Trail.Controls.ControlList();
            this.SuspendLayout();
            // 
            // actionsList
            // 
            this.actionsList.AlternateBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(58)))));
            this.actionsList.AutoScroll = true;
            this.actionsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsList.Location = new System.Drawing.Point(0, 27);
            this.actionsList.Name = "actionsList";
            this.actionsList.Size = new System.Drawing.Size(275, 289);
            this.actionsList.TabIndex = 1;
            // 
            // ActionQueueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 316);
            this.ControlBoxText = "Action Queue";
            this.Controls.Add(this.actionsList);
            this.Name = "ActionQueueForm";
            this.Controls.SetChildIndex(this.actionsList, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ControlList actionsList;
    }
}