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
            tvSidebar.LoadDrives();
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            cvColumns.ScrollToLastColumn();
        }

        private void tvSidebar_AfterSelect(object sender, TreeViewEventArgs e) {
            ColumnTreeNode node = e.Node as ColumnTreeNode;
            if (node.Column == null) return;

            cvColumns.Columns.Clear();
            cvColumns.Columns.Add(node.Column);
            (node.Column as ItemsColumn).LoadItems();
        }
    }
}
