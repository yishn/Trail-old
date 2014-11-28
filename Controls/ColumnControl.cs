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
        public string HeaderText { get { return colHeader.Text; } set { colHeader.Text = value; } }
        public bool Disabled { get { return !ListViewControl.Visible; } set { ListViewControl.Visible = !value; } }

        public ColumnControl() {
            InitializeComponent();
        }

        private void lvList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
            int padding = this.Width - ListViewControl.ClientSize.Width;
            this.Width = e.NewWidth + padding;
        }

        private void ListViewControl_ClientSizeChanged(object sender, EventArgs e) {
            colHeader.Width = ListViewControl.ClientSize.Width;
        }
    }
}
