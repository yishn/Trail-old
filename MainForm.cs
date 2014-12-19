using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            tabBar.Tabs.CollectionChanged += Tabs_CollectionChanged;

            LoadPreferences();
        }

        public void LoadPreferences() {
            try {
                Persistence.LoadData();

                List<int> size = Persistence.GetPreferenceList<int>("window.size");
                this.Size = new Size(size[0], size[1]);
                splitContainer.SplitterDistance = Persistence.GetPreference<int>("sidebar.width");

                sidebar.Load();
                tabBar.LoadSession();
            } catch (Exception) {
                MessageBox.Show("An error occured while loading preferences.", "Trail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action != NotifyCollectionChangedAction.Add) return;

            foreach (Tab t in e.NewItems) {
                // Prepare tab.ColumnView

                NavigatingTab tab = t as NavigatingTab;
                tab.ColumnView.Dock = DockStyle.Fill;
                tab.ColumnView.ItemsIconQueue = iconQueue;
                tab.ColumnView.ImageList = itemsImages;
                tab.ColumnView.SubColumnAdded += ColumnView_SubColumnAdded;

                splitContainer.Panel2.Controls.Add(tab.ColumnView);
                tab.ColumnView.BringToFront();
            }
        }

        private void sidebar_AfterSelect(object sender, TreeViewEventArgs e) {
            ColumnTreeNode node = e.Node as ColumnTreeNode;
            if (node.SubColumn == null) return;

            ItemsColumn c = node.SubColumn.Duplicate();
            tabBar.CurrentTab.Text = c.HeaderText;
            tabBar.CurrentTab.ColumnView.NavigateTo(c.GetTrail());
            tabBar.CurrentTab.ColumnView.Columns[0].Focus();
            tabBar.CurrentTab.ColumnView.ScrollToLastColumn();

            sidebar.SelectedNode = null;
        }

        private void ColumnView_SubColumnAdded(object sender, ItemsColumn column) {
            tabBar.CurrentTab.Text = column.HeaderText;
        }

        private void tabBar_AddButtonClicked(object sender, EventArgs e) {
            NavigatingTab t = new NavigatingTab(new EmptyColumn());
            tabBar.AddTab(t);
            tabBar.CurrentTab = t;
        }

        #region Update preferences

        private void MainForm_ResizeEnd(object sender, EventArgs e) {
            if (!Persistence.GetPreference<bool>("window.remember_size")) return;
            Persistence.SetPreference("window.size", new List<object>(new object[] { this.Width, this.Height }));
        }

        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e) {
            if (!Persistence.GetPreference<bool>("sidebar.remember_width")) return;
            Persistence.SetPreference("sidebar.width", sidebar.Width);
        }

        #endregion

        #region Menu events

        private void aboutTrailToolStripMenuItem_Click(object sender, EventArgs e) {
            new AboutForm().ShowDialog();
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e) {
            tabBar_AddButtonClicked(sender, e);
        }

        private void nextTabToolStripMenuItem_Click(object sender, EventArgs e) {
            int index = tabBar.Tabs.IndexOf(tabBar.CurrentTab);
            tabBar.CurrentTab = tabBar.Tabs[(index + 1) % tabBar.Tabs.Count] as NavigatingTab;
        }

        private void previousTabToolStripMenuItem_Click(object sender, EventArgs e) {
            int index = tabBar.Tabs.IndexOf(tabBar.CurrentTab);
            tabBar.CurrentTab = tabBar.Tabs[(tabBar.Tabs.Count + index - 1) % tabBar.Tabs.Count] as NavigatingTab;
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e) {
            tabBar.CloseTab(tabBar.CurrentTab);
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e) {
            NavigatingTab t = new NavigatingTab(new DirectoryColumn(Persistence.PersistenceFolder));
            tabBar.AddTab(t);
            tabBar.CurrentTab = t;
        }

        private void duplicateTabToolStripMenuItem_Click(object sender, EventArgs e) {
            NavigatingTab t = new NavigatingTab(tabBar.CurrentTab.ColumnView.LastColumn.Duplicate());
            tabBar.AddTab(t);
            tabBar.CurrentTab = t;
        }

        private void goToPathToolStripMenuItem_Click(object sender, EventArgs e) {
            GotoForm form = new GotoForm();
            form.ItemsPath = tabBar.CurrentTab.ColumnView.LastColumn.ItemsPath;
            form.Left = this.Left + this.Width / 2 - form.Width / 2;
            form.Top = this.Top + this.Height / 4 - form.Height / 4;
            form.Show();

            form.AcceptButtonClicked += (_, evt) => {
                tabBar.CurrentTab.ColumnView.NavigateTo(new DirectoryColumn(form.ItemsPath));
                tabBar.CurrentTab.ColumnView.Columns[0].Focus();
                tabBar.CurrentTab.ColumnView.ScrollToLastColumn();
            };
        }

        #endregion
    }
}
