using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public partial class ChoiceDialog : Form {
        public ChoiceDialog() {
            InitializeComponent();
        }
        public ChoiceDialog(string title, string okButton, string yesButton, string noButton, string cancelButton) : this() {
            Text = title;
            this.okButton.Text = okButton;
            this.yesButton.Text = yesButton;
            this.noButton.Text = noButton;
            this.cancelButton.Text = cancelButton;
        }

        public DialogResult ShowDialog(string headerText) {
            headerLabel.Text = headerText;
            return base.ShowDialog();
        }
    }
}
