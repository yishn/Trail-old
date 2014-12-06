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
    public partial class Tab : UserControl {
        public override string Text { get { return lblText.Text; } set { lblText.Text = value; } }
        public bool Current { get; set; }

        public Tab() {
            InitializeComponent();

            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 255, 255, 255);
            btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0, 0);
        }

        private void lblText_SizeChanged(object sender, EventArgs e) {
            this.Width = lblText.Width + btnClose.Width + 10;
        }
    }
}
