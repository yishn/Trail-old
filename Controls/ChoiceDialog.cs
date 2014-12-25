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
        public ChoiceDialog(string title) : this() {
            this.Text = title;
        }

        public DialogResult ShowDialog(string descriptionText, string headerText,
            string okButton, string yesButton, string noButton, string cancelButton) {
            this.descriptionLabel.Text = descriptionText;
            this.headerLabel.Text = headerText;
            this.okButton.Text = okButton;
            this.yesButton.Text = yesButton;
            this.noButton.Text = noButton;
            this.cancelButton.Text = cancelButton;

            return base.ShowDialog();
        }
    }
}
