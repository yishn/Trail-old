using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public class ControlList : Panel {
        private Color alternateBackColor;

        public bool NewItemsOnTop { get; set; }
        public Color AlternateBackColor {
            get { return alternateBackColor; }
            set { alternateBackColor = value; Recolor(); }
        }

        public ControlList() {
            alternateBackColor = this.BackColor;

            this.ControlAdded += ControlList_ControlAdded;
            this.ControlRemoved += ControlList_ControlRemoved;
        }

        private void ControlList_ControlAdded(object sender, ControlEventArgs e) {
            this.SuspendLayout();
            if (!NewItemsOnTop) e.Control.BringToFront();
            else e.Control.SendToBack();
            Rearrange();
            Recolor();
            this.ResumeLayout();
        }

        private void ControlList_ControlRemoved(object sender, ControlEventArgs e) {
            Rearrange();
            Recolor();
        }

        public void Rearrange() {
            this.SuspendLayout();
            for (int i = 0; i < this.Controls.Count; i++) {
                this.Controls[i].Dock = DockStyle.Top;
            }
            this.ResumeLayout();
        }

        public void Recolor() {
            for (int i = 1; i <= this.Controls.Count; i++) {
                this.Controls[this.Controls.Count - i].BackColor = i % 2 == 0 ? AlternateBackColor : BackColor;
            }
        }
    }
}
