using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;
using Trail.Controls;
using Trail.DataTypes;

namespace Trail.Modules {
    public class NavigatingTabBar : TabBar {
        private Stack<NavigatingTab> closedTabs = new Stack<NavigatingTab>();

        public new NavigatingTab CurrentTab { get { return base.CurrentTab as NavigatingTab; } set { base.CurrentTab = value; } }

        public NavigatingTabBar() {
            this.CurrentTabChanged += NavigatingTabBar_CurrentTabChanged;

            this.TabAdded += NavigatingTabBar_TabAddedMoved;
            this.TabMoved += NavigatingTabBar_TabAddedMoved;
            this.TabClosed += NavigatingTabBar_TabClosed;
            this.Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action != NotifyCollectionChangedAction.Add) return;

            foreach (Tab t in e.NewItems) {
                (t as NavigatingTab).Navigated += NavigatingTabBar_Navigated;
            }
        }

        private void NavigatingTabBar_Navigated(object sender, EventArgs e) {
            this.SaveSession();
        }

        private void NavigatingTabBar_TabAddedMoved(object sender, Tab e) {
            this.SaveSession();
        }

        private void NavigatingTabBar_TabClosed(object sender, Tab e) {
            closedTabs.Push(e as NavigatingTab);
            this.SaveSession();
        }

        public void LoadSession() {
            this.Tabs.Clear();
            closedTabs.Clear();

            foreach (ColumnData data in Persistence.Session) {
                ItemsColumn column = data.Instantiation();
                NavigatingTab tab = new NavigatingTab(column);
                this.Tabs.Add(tab);
            }

            int index =  Persistence.GetPreference<int>("tabbar.tab_index");
            index = Math.Max(0, Math.Min(index, this.Tabs.Count - 1));
            base.CurrentTab = this.Tabs[index];

            this.CurrentTab.ColumnView.Load += (_, e) => {
                this.CurrentTab.ColumnView.ScrollToLastColumn();
            };
        }

        public void SaveSession() {
            Persistence.Session.Clear();

            for (int i = 0; i < this.Tabs.Count; i++) {
                Persistence.Session.Add((this.Tabs[i] as NavigatingTab).ColumnView.LastColumn.GetColumnData());
            }

            Persistence.SetPreference("tabbar.tab_index", this.Tabs.IndexOf(this.CurrentTab));
            Persistence.SaveData();
        }

        private void NavigatingTabBar_CurrentTabChanged(object sender, EventArgs e) {
            if (this.CurrentTab == null) return;
            if (this.CurrentTab.ColumnView.LastColumn != null)
                this.CurrentTab.ColumnView.LastColumn.Focus();

            this.CurrentTab.ColumnView.BringToFront();
            SaveSession();
        }

        public void RestoreClosedTab() {
            if (closedTabs.Count == 0) return;
            NavigatingTab tab = closedTabs.Peek();
            if (this.AddTab(tab)) this.CurrentTab = closedTabs.Pop();
        }
    }
}
