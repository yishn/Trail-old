using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Fx;

namespace Trail.Controls {
    public partial class HudForm : Form {
        private Animation animation = new Animation();
        private double opacity;

        public new string Text { get { return controlBoxLabel.Text; } set { controlBoxLabel.Text = value; } }

        public HudForm() {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        #region Form opacity animations

        private void HelperForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (animation.Enabled) return;
            if (this.Opacity == 0) return;

            animation = new Animation();
            e.Cancel = true;

            opacity = this.Opacity;
            animation.Tick += (_, value) => {
                this.Opacity = opacity - (opacity * value);
            };
            animation.Complete += (_, evt) => {
                this.Close();
            };
            animation.Start();
        }

        private void HelperForm_Shown(object sender, EventArgs e) {
            if (animation.Enabled) return;

            animation = new Animation();
            animation.Start().Tick += (_, value) => {
                this.Opacity = opacity * value;
            };
        }

        private void HelperForm_Load(object sender, EventArgs e) {
            opacity = this.Opacity;
            this.Opacity = 0;
        }

        #endregion

        #region Move form

        Point mouseOldPoint;

        private void controlBoxLabel_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;
            this.Location += new Size(e.Location - new Size(mouseOldPoint));
        }

        private void controlBoxLabel_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;
            mouseOldPoint = e.Location;
        }

        #endregion
    }
}
