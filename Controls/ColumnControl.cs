using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Trail.Controls {
    public partial class ColumnControl : UserControl {
        public string HeaderText { get { return headerColumn.Text; } set { headerColumn.Text = value; } }
        public bool ShowError { get { return errorPanel.Visible; } set { errorPanel.Visible = value; } }
        public string ErrorText { get { return errorMessageLabel.Text; } set { errorMessageLabel.Text = value; } }

        public event EventHandler<ListViewItem> OneItemSelected;

        public ColumnControl() {
            InitializeComponent();
        }

        private void ListViewControl_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
            int padding = this.Width - ListViewControl.ClientSize.Width;
            this.Width = e.NewWidth + padding + 3;
            (this.Parent.Parent as ColumnView).EnlargeScrollMinSize();
        }

        private void ListViewControl_ClientSizeChanged(object sender, EventArgs e) {
            headerColumn.Width = ListViewControl.ClientSize.Width - 3;
        }

        public void UpdateColumnWidth() {
            this.SuspendLayout();
            ListViewControl.BeginUpdate();
            ListViewControl.Width++;
            ListViewControl.Width--;
            ListViewControl.EndUpdate();
            this.ResumeLayout();
        }

        #region OneItemSelected event

        private bool changedSelection = false;

        private void ListViewControl_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left || !changedSelection) return;
            if (ListViewControl.SelectedItems.Count != 1) return;

            if (OneItemSelected != null) OneItemSelected(this, ListViewControl.SelectedItems[0]);
            changedSelection = false;
        }

        private void ListViewControl_SelectedIndexChanged(object sender, EventArgs e) {
            changedSelection = true;
        }

        private void ListViewControl_ItemActivate(object sender, EventArgs e) {
            if (ListViewControl.SelectedItems.Count != 1) return;
            if (OneItemSelected != null) OneItemSelected(this, ListViewControl.SelectedItems[0]);
        }

        #endregion
    }
}
