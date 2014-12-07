using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;
using Trail.Controls;

namespace Trail.Modules {
    public class NavigatingColumnView : ColumnView {
        private ItemsIconQueue _iconQueue = new ItemsIconQueue();

        public event EventHandler<ItemsColumn> SubColumnAdded;
        
        public NavigatingColumnView() {
            this.BackColor = Color.White;

            this.SubColumnAdded += NavigatingColumnView_SubColumnAdded;
            this.Columns.CollectionChanged += Columns_CollectionChanged;
        }

        public void LoadIcons() {
            _iconQueue.ImageList = this.ImageList;

            foreach (ItemsColumn c in this.Columns) {
                _iconQueue.Enqueue(c);
            }
        }

        public void NavigateTo(DirectoryInfo directory) {
            List<ItemsColumn> trail = new List<ItemsColumn>();
            DirectoryInfo current = directory;

            while (current.Root.FullName != current.FullName) {
                trail.Add(new DirectoryColumn(current));
                current = current.Parent;
            }

            trail.Reverse();
            this.NavigateTo(trail);
        }
        public void NavigateTo(List<ItemsColumn> trail) {
            this.Columns.Clear();

            foreach (ItemsColumn c in trail) {
                this.Columns.Add(c);
                c.LoadItems();
            }
        }

        private void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            
            foreach (ItemsColumn c in e.NewItems) {
                c.LoadingCompleted += ItemsColumn_LoadingCompleted;
                c.ListViewControl.SelectedIndexChanged += (_, evt) => {
                    ItemsColumn_SelectedIndexChanged(c, evt);
                };
            }
        }

        private void ItemsColumn_LoadingCompleted(object sender, RunWorkerCompletedEventArgs e) {
            // Load icons
            _iconQueue.ImageList = this.ImageList;
            _iconQueue.Enqueue(sender as ItemsColumn);
        }

        private void ItemsColumn_SelectedIndexChanged(object sender, EventArgs e) {
            ColumnControl c = sender as ColumnControl;
            if (c.ListViewControl.SelectedIndices.Count == 0) return;
            ColumnListViewItem item = c.ListViewControl.SelectedItems[0] as ColumnListViewItem;
            if (item.SubColumn == null) return;

            int i = this.Columns.IndexOf(c);
            if (this.Columns.Count > i + 1 
                && (this.Columns[i + 1] as ItemsColumn).ItemsPath == (item.SubColumn as ItemsColumn).ItemsPath) return;

            // Remove columns on the right
            int residueCount = this.Columns.Count - i - 1;
            int width = residueCount > 0 ? this.Columns[i + 1].Width : this.DefaultColumnWidth;
            this.ScrollPanel.SuspendLayout();

            UpdateScrollMinSize();

            for (int j = 1; j <= residueCount; j++)
                this.Columns.RemoveAt(i + 1);

            if (c.ListViewControl.SelectedItems.Count == 1) {
                // Add new column
                ItemsColumn column = (item.SubColumn as ItemsColumn).Duplicate();

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
        }
    }
}
