using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;

namespace Trail.Forms {
    public partial class AboutForm : Form {
        public AboutForm() {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e) {
            labelVersion.Text = "v" + Application.ProductVersion;
            labelCopyright.Text = "Copyright © Yichuan Shen " + DateTime.Now.Year;
        }

        private void labelCopyright_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://yichuanshen.de");
        }

        private void labelGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://github.com/yishn/Trail");
        }
    }
}
