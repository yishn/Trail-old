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
        public ListView.ListViewItemCollection Items { get { return lvList.Items; } }
   
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        public ColumnControl() {
            InitializeComponent();
            SetWindowTheme(lvList.Handle, "Explorer", null);
        }

        private void ColumnControl_ClientSizeChanged(object sender, EventArgs e) {
            colHeader.Width = lvList.ClientSize.Width;
        }
    }
}
