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
            columnView.ScrollToLastColumn();
        }

        private void tvSidebar_AfterSelect(object sender, TreeViewEventArgs e) {
            ColumnTreeNode node = e.Node as ColumnTreeNode;
            if (node.SubColumn == null) return;

            columnView.Columns.Clear();
            columnView.Columns.Add(node.SubColumn);

            ItemsColumn c = node.SubColumn as ItemsColumn;
            c.LoadItems();

            columnView.ScrollToFirstColumn();
        }
    }
}
