using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Fx;

namespace Trail.Modules {
    public partial class FilterToolbar : UserControl {
        private IntAnimation animation = new IntAnimation();

        public event EventHandler CancelButtonClicked;
        public event EventHandler FilterTextChanged;

        public string FilterText { get { return filterTextBox.Text; } set { filterTextBox.Text = value; } }

        public FilterToolbar() {
            InitializeComponent();
        }

        public void SlideOpen() {
            if (animation.Enabled) return;

            animation = new IntAnimation();
            animation.Start(0, 30).Tick += (_, value) => {
                this.Height = value;
            };
        }

        public void SlideClose() {
            if (animation.Enabled) return;

            FilterText = "";

            animation = new IntAnimation();
            animation.Start(30, 0).Tick += (_, value) => {
                this.Height = value;
            };
        }

        private void cancelButton_MouseEnter(object sender, EventArgs e) {
            cancelButton.ForeColor = Color.White;
        }

        private void cancelButton_MouseLeave(object sender, EventArgs e) {
            cancelButton.ForeColor = this.ForeColor;
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            if (CancelButtonClicked != null) CancelButtonClicked(this, e);
        }

        private void filterTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) e.SuppressKeyPress = true;
        }

        private void filterTextBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape && CancelButtonClicked != null) CancelButtonClicked(this, e);
        }

        private void label1_Click(object sender, EventArgs e) {
            filterTextBox.Focus();
        }

        private void FindToolbar_Enter(object sender, EventArgs e) {
            filterTextBox.Focus();
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e) {
            if (FilterTextChanged != null) FilterTextChanged(this, e);
        }
    }
}
