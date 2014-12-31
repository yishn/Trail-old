using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using Trail.Controls;
using Trail.DataTypes;
using Trail.Templates;

namespace Trail.Modules {
    public class NavigatingColumnView : ColumnView {
        public ItemsIconQueue ItemsIconQueue { get; set; }
        public new ItemsColumn LastColumn { get { return base.LastColumn as ItemsColumn; } }
        public IHost Host { get { return this.ParentForm as IHost; } }

        public event EventHandler<ItemsColumn> SubColumnAdded;
        public event EventHandler Navigated;
        
        public NavigatingColumnView() {
            this.BackColor = Color.White;
            this.DefaultColumnWidth = Persistence.GetPreference<int>("column.default_width");

            this.SubColumnAdded += NavigatingColumnView_SubColumnAdded;
            this.Columns.CollectionChanged += Columns_CollectionChanged;
        }

        public void LoadIcons() {
            if (ItemsIconQueue == null) return;
            ItemsIconQueue.ImageList = this.ImageList;

            foreach (ItemsColumn c in this.Columns) {
                ItemsIconQueue.Enqueue(c);
            }
        }

        public void NavigateTo(List<ItemsColumn> trail) {
            this.Columns.Clear();

            foreach (ItemsColumn c in trail) {
                this.Columns.Add(c);
                c.Load += (_, __) => { c.LoadItems(); };
            }

            if (Navigated != null) Navigated(this, EventArgs.Empty);
        }
        public void NavigateTo(ItemsColumn column) { NavigateTo(column.GetTrail()); }

        public void SortAll() {
            foreach (ItemsColumn c in this.Columns) {
                c.ListViewControl.Sort();

                if (c.ListViewControl.Items.Count == 0) continue;
                c.ListViewControl.Items[0].EnsureVisible();
            }
        }

        private void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            
            foreach (ItemsColumn c in e.NewItems) {
                c.ListViewControl.ListViewItemSorter = new FilterListComparer();

                c.LoadingCompleted += ItemsColumn_LoadingCompleted;
                c.OneItemSelected += ItemsColumn_OneItemSelected;
            }
        }

        private void ItemsColumn_LoadingCompleted(object sender, EventArgs e) {
            if (ItemsIconQueue == null) return;
            ItemsIconQueue.ImageList = this.ImageList;
            ItemsIconQueue.Enqueue(sender as ItemsColumn);
        }

        private void ItemsColumn_OneItemSelected(object sender, ListViewItem e) {
            ColumnControl c = sender as ColumnControl;
            ColumnListViewItem item = e as ColumnListViewItem;
            if (item.SubColumn == null) return;

            // Don't add existing column
            int i = this.Columns.IndexOf(c);
            if (this.Columns.Count > i + 1 
                && (this.Columns[i + 1] as ItemsColumn).ItemsPath == item.SubColumn.Path) return;

            // Remove columns on the right
            int residueCount = this.Columns.Count - i - 1;
            int width = residueCount > 0 ? this.Columns[i + 1].Width : this.DefaultColumnWidth;
            this.ScrollPanel.SuspendLayout();

            UpdateScrollMinSize();

            for (int j = 1; j <= residueCount; j++)
                this.Columns.RemoveAt(i + 1);

            if (c.ListViewControl.SelectedItems.Count == 1) {
                // Add new column
                ItemsColumn column = Packages.InstantiateColumn(item.SubColumn, Host);

                this.Columns.Add(column);
                column.Width = width;
                this.ScrollPanel.ResumeLayout();
                if (SubColumnAdded != null) SubColumnAdded(this, column);
            } else {
                this.ScrollPanel.ResumeLayout();
            }
        }

        private void NavigatingColumnView_SubColumnAdded(object sender, ItemsColumn column) {
            column.LoadItems();
            this.ScrollToLastColumn();
            if (Navigated != null) Navigated(this, EventArgs.Empty);
        }
    }
}
