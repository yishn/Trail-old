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
        public MainForm() {
            InitializeComponent();

            Persistence.LoadData();
            LoadPreferences();

            sidebar.Load();
        }

        public void LoadPreferences() {
            List<int> size = Persistence.GetPreferenceList<int>("window.size");
            this.Size = new Size(size[0], size[1]);
            splitContainer.SplitterDistance = Persistence.GetPreference<int>("sidebar.width");
        }

        private void MainForm_Load(object sender, EventArgs e) {
            tabBar_AddButtonClicked(tabBar, EventArgs.Empty);
        }

        private void sidebar_AfterSelect(object sender, TreeViewEventArgs e) {
            ColumnTreeNode node = e.Node as ColumnTreeNode;
            if (node.SubColumn == null) return;

            ItemsColumn c = node.SubColumn.Duplicate();
            tabBar.CurrentTab.ColumnView.NavigateTo(c.GetTrail());
            tabBar.CurrentTab.Text = c.HeaderText;
            tabBar.CurrentTab.ColumnView.ScrollToLastColumn();

            sidebar.SelectedNode = null;
        }

        private void columnView_SubColumnAdded(object sender, ItemsColumn column) {
            tabBar.CurrentTab.Text = column.HeaderText;
        }

        private void tabBar_AddButtonClicked(object sender, EventArgs e) {
            NavigatingColumnView columnView = new NavigatingColumnView() {
                Dock = DockStyle.Fill,
                ImageList = itemsImages
            };
            columnView.SubColumnAdded += columnView_SubColumnAdded;
            splitContainer.Panel2.Controls.Add(columnView);
            columnView.BringToFront();

            NavigatingTab t = new NavigatingTab() {
                Text = "New Tab",
                ColumnView = columnView
            };
            tabBar.AddTab(t);
            tabBar.CurrentTab = t;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e) {
            if (!Persistence.GetPreference<bool>("window.remember_size")) return;
            Persistence.SetPreference("window.size", new List<object>(new object[] { this.Width, this.Height }));
        }

        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e) {
            if (!Persistence.GetPreference<bool>("sidebar.remember_width")) return;
            Persistence.SetPreference("sidebar.width", sidebar.Width);
        }
    }
}
