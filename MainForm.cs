using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Columns;
using Trail.Controls;
using Trail.Modules;

namespace Trail {
    public partial class MainForm : Form {
        public static Persistence Persistence = new Persistence();

        public MainForm() {
            InitializeComponent();

            tabBar1_AddButtonClicked(tabBar1, EventArgs.Empty);
        }

        private void MainForm_Load(object sender, EventArgs e) {
            tvSidebar.Load();
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            tabBar1.CurrentTab.ColumnView.ScrollToLastColumn();
        }

        private void tvSidebar_AfterSelect(object sender, TreeViewEventArgs e) {
            ColumnTreeNode node = e.Node as ColumnTreeNode;
            if (node.SubColumn == null) return;

            ItemsColumn c = node.SubColumn.Duplicate();
            tabBar1.CurrentTab.ColumnView.NavigateTo(c.GetTrail());
            tabBar1.CurrentTab.Text = c.HeaderText;
            tabBar1.CurrentTab.ColumnView.ScrollToLastColumn();

            tvSidebar.SelectedNode = null;
        }

        private void columnView_SubColumnAdded(object sender, ItemsColumn column) {
            tabBar1.CurrentTab.Text = column.HeaderText;
        }

        private void tabBar1_AddButtonClicked(object sender, EventArgs e) {
            NavigatingColumnView columnView = new NavigatingColumnView() {
                Dock = DockStyle.Fill,
                ImageList = ilItems
            };
            columnView.SubColumnAdded += columnView_SubColumnAdded;
            pnlSplit.Panel2.Controls.Add(columnView);
            columnView.BringToFront();

            NavigatingTab t = new NavigatingTab() { 
                Text = "New Tab",
                ColumnView = columnView
            };
            tabBar1.AddTab(t);
            tabBar1.CurrentTab = t;
        }
    }
}
