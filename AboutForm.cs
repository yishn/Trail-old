using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;

namespace Trail {
    public partial class AboutForm : HudForm {
        public AboutForm() {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e) {
            labelVersion.Text = "v" + Application.ProductVersion;
        }
    }
}
