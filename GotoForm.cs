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
    public partial class GotoForm : HudForm {
        public string ItemsPath { 
            get { return pathTextBox.Text; }
            set { pathTextBox.Text = value; pathTextBox.SelectAll(); } 
        }

        public event EventHandler AcceptButtonClicked;

        public GotoForm() {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e) {
            if (AcceptButtonClicked != null) AcceptButtonClicked(this, e);
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void GotoForm_Deactivate(object sender, EventArgs e) {
            this.Close();
        }
    }
}
