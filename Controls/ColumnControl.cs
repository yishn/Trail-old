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
        public ImageList ImageList { get { return lvList.SmallImageList; } set { lvList.SmallImageList = value; } }
        public ListView.ListViewItemCollection Items { get { return lvList.Items; } }

        public ColumnControl() {
            InitializeComponent();
        }

        private void ColumnControl_ClientSizeChanged(object sender, EventArgs e) {
            colHeader.Width = lvList.ClientSize.Width;
        }

        private void lvList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
            int padding = this.Width - lvList.ClientSize.Width;
            this.Width = e.NewWidth + padding;
        }
    }
}
