using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Columns;
using Trail.Controls;

namespace Trail {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            DirectoryColumn c = new DirectoryColumn(new DirectoryInfo("D:\\"));
            cvColumns.Columns.Add(c);
            c.LoadItems();

            tvSidebar.LoadDrives();
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            cvColumns.ScrollToLastColumn();
        }
    }
}
