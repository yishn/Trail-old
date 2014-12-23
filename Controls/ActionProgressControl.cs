using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public partial class ActionProgressControl : UserControl {
        public string HeaderText { get { return headerLabel.Text; } set { headerLabel.Text = value; } }
        public string DescriptionText { get { return descriptionLabel.Text; } set { descriptionLabel.Text = value; } }
        public Color AccentColor { get { return progressBarValue.BackColor; } set { progressBarValue.BackColor = value; } }
        public int Progress {
            get { return (int)(100.0 * progressBarValue.Width / progressBar.Width); }
            set { progressBarValue.Width = (int)(progressBar.Width * value / 100.0); }
        }

        public ActionProgressControl() {
            InitializeComponent();
        }

        protected override void OnMouseLeave(EventArgs e) {
            if (this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
                return;
            else {
                base.OnMouseLeave(e);
            }
        }

        private void ActionProgressControl_MouseEnter(object sender, EventArgs e) {
            cancelButton.Visible = true;
        }

        private void ActionProgressControl_MouseLeave(object sender, EventArgs e) {
            cancelButton.Visible = false;
        }

        private void Control_MouseEnter(object sender, EventArgs e) {
            OnMouseEnter(e);
        }

        private void Control_MouseLeave(object sender, EventArgs e) {
            OnMouseLeave(e);
        }
    }
}
