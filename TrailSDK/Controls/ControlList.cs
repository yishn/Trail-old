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
            alternateBackColor = BackColor;

            ControlAdded += ControlList_ControlAdded;
            ControlRemoved += ControlList_ControlRemoved;
        }

        private void ControlList_ControlAdded(object sender, ControlEventArgs e) {
            SuspendLayout();
            if (!NewItemsOnTop) e.Control.BringToFront();
            else e.Control.SendToBack();
            Rearrange();
            Recolor();
            ResumeLayout();
        }

        private void ControlList_ControlRemoved(object sender, ControlEventArgs e) {
            Rearrange();
            Recolor();
        }

        public void Rearrange() {
            SuspendLayout();
            for (int i = 0; i < Controls.Count; i++) {
                Controls[i].Dock = DockStyle.Top;
            }
            ResumeLayout();
        }

        public void Recolor() {
            for (int i = 1; i <= Controls.Count; i++) {
                Controls[Controls.Count - i].BackColor = i % 2 == 0 ? AlternateBackColor : BackColor;
            }
        }
    }
}
