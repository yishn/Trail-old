using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using Trail.Actions;
using Trail.Columns;
using Trail.Controls;
using Trail.DataTypes;
using Trail.Forms;
using Trail.Modules;

namespace Trail {
    public partial class MainForm : Form, IHost {
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
                splitContainer.Panel1Collapsed = !Persistence.GetPreference<bool>("sidebar.visible");
                tabBar.Visible = Persistence.GetPreference<bool>("tabbar.visible");

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

                splitContainer.Panel2.Controls.Add(tab.ColumnView);
                tab.ColumnView.BringToFront();
            }
        }

        private void sidebar_AfterSelect(object sender, TreeViewEventArgs e) {
            ColumnTreeNode node = e.Node as ColumnTreeNode;
            if (node.SubColumn == null) return;

            ItemsColumn c = node.SubColumn.Instantiation(this);
            tabBar.CurrentTab.ColumnView.NavigateTo(c.GetTrail());
            tabBar.CurrentTab.ColumnView.Columns[0].Focus();
            tabBar.CurrentTab.ColumnView.ScrollToLastColumn();

            sidebar.SelectedNode = null;
        }

        private void tabBar_AddButtonClicked(object sender, EventArgs e) {
            NavigatingTab t = new NavigatingTab(new EmptyColumn(this));
            if (tabBar.AddTab(t)) tabBar.CurrentTab = t;
        }

        #region Update preferences

        private void MainForm_ResizeEnd(object sender, EventArgs e) {
            if (this.WindowState != FormWindowState.Normal) return;
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
            NavigatingTab t = new NavigatingTab(new DirectoryColumn(Persistence.PersistenceFolder, this));
            if (tabBar.AddTab(t)) tabBar.CurrentTab = t;
            t.ColumnView.Columns[0].Focus();
            t.ColumnView.ScrollToLastColumn();
        }

        private void duplicateTabToolStripMenuItem_Click(object sender, EventArgs e) {
            NavigatingTab t = new NavigatingTab(tabBar.CurrentTab.ColumnView.LastColumn.Duplicate());
            if (tabBar.AddTab(t)) tabBar.CurrentTab = t;
            t.ColumnView.Columns[0].Focus();
            t.ColumnView.ScrollToLastColumn();
        }

        private void goToLocationToolStripMenuItem_Click(object sender, EventArgs e) {
            GotoForm form = new GotoForm();
            if (tabBar.CurrentTab.ColumnView.LastColumn is DirectoryColumn)
                form.ItemsPath = tabBar.CurrentTab.ColumnView.LastColumn.ItemsPath;
            form.Left = this.Left + this.Width / 2 - form.Width / 2;
            form.Top = this.Top + this.Height / 4 - form.Height / 4;
            form.Show();

            form.AcceptButtonClicked += (_, evt) => {
                tabBar.CurrentTab.ColumnView.NavigateTo(new DirectoryColumn(form.ItemsPath, this));
                tabBar.CurrentTab.ColumnView.Columns[0].Focus();
                tabBar.CurrentTab.ColumnView.ScrollToLastColumn();
            };
        }

        private void restoreClosedTabToolStripMenuItem_Click(object sender, EventArgs e) {
            tabBar.RestoreClosedTab();
        }

        private void exitTrailToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            sidebarToolStripMenuItem.Checked = !splitContainer.Panel1Collapsed;
            tabBarToolStripMenuItem.Checked = tabBar.Visible;
            //actionQueueToolStripMenuItem.Checked = actionQueuePanel.Visible;
        }

        private void sidebarToolStripMenuItem_Click(object sender, EventArgs e) {
            splitContainer.Panel1Collapsed = !splitContainer.Panel1Collapsed;
            Persistence.SetPreference("sidebar.visible", !splitContainer.Panel1Collapsed);
        }

        private void tabBarToolStripMenuItem_Click(object sender, EventArgs e) {
            tabBar.Visible = !tabBar.Visible;
            Persistence.SetPreference("tabbar.visible", tabBar.Visible);
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Restart();
        }

        #endregion

        #region IHost implementation

        void IHost.SetPreference(string key, object value) {
            Persistence.SetPreference(key, value);
        }

        string IHost.GetPreference(string key) {
            return Persistence.GetPreference(key);
        }

        T IHost.GetPreference<T>(string key) {
            return Persistence.GetPreference<T>(key);
        }

        List<string> IHost.GetPreferenceList(string key) {
            return Persistence.GetPreferenceList(key);
        }

        List<T> IHost.GetPreferenceList<T>(string key) {
            return Persistence.GetPreferenceList<T>(key);
        }

        void IHost.EnqueueAction(IAction action) {
            actionQueueList.EnqueueAction(new ActionControl(action));
        }

        #endregion
    }
}
