using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;
using Trail.Controls;

namespace Trail.Modules {
    public class NavigatingColumnView : ColumnView {
        private IconLoaderQueue _iconLoaderQueue = new IconLoaderQueue();

        public event EventHandler<ColumnEventArgs> SubColumnAdded;
        
        public NavigatingColumnView() {
            this.SubColumnAdded += NavigatingColumnView_SubColumnAdded;
            this.Columns.CollectionChanged += Columns_CollectionChanged;
        }

        public void LoadIcons() {
            _iconLoaderQueue.ImageList = this.ImageList;

            foreach (ItemsColumn c in this.Columns) {
                _iconLoaderQueue.Enqueue(c);
            }
        }

        public void NavigateTo(DirectoryInfo directory) {
            List<DirectoryInfo> trail = new List<DirectoryInfo>();
            DirectoryInfo current = directory;
            this.Columns.Clear();

            while (current.Root.FullName != current.FullName) {
                trail.Add(current);
                current = current.Parent;
            }

            trail.Reverse();

            foreach (DirectoryInfo dI in trail) {
                DirectoryColumn c = new DirectoryColumn(dI);
                this.Columns.Add(c);
                c.LoadItems();
            }
        }

        private void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (ItemsColumn c in e.NewItems) {
                    c.LoadingCompleted += ItemsColumn_LoadingCompleted;
                    c.ListViewControl.SelectedIndexChanged += (_, evt) => {
                        ItemsColumn_SelectedIndexChanged(c, evt);
                    };
                }
            }
        }

        private void ItemsColumn_LoadingCompleted(object sender, RunWorkerCompletedEventArgs e) {
            // Load icons
            _iconLoaderQueue.ImageList = this.ImageList;
            _iconLoaderQueue.Enqueue(sender as ItemsColumn);
        }

        private void ItemsColumn_SelectedIndexChanged(object sender, EventArgs e) {
            ColumnControl c = sender as ColumnControl;
            if (c.ListViewControl.SelectedIndices.Count == 0) return;
            ColumnListViewItem item = c.ListViewControl.SelectedItems[0] as ColumnListViewItem;
            if (item.Column == null) return;

            int i = this.Columns.IndexOf(c);
            if (this.Columns.Count > i + 1 && this.Columns[i + 1] == item.Column) return;

            // Remove columns on the right
            int residueCount = this.Columns.Count - i - 1;
            int width = residueCount > 0 ? this.Columns[i + 1].Width : this.DefaultColumnWidth;
            this.ScrollPanel.SuspendLayout();

            UpdateScrollMinSize();

            for (int j = 1; j <= residueCount; j++)
                this.Columns.RemoveAt(i + 1);

            if (c.ListViewControl.SelectedItems.Count == 1) {
                // Add new column
                this.Columns.Add(item.Column);
                item.Column.Width = width;
                this.ScrollPanel.ResumeLayout();
                if (SubColumnAdded != null) SubColumnAdded(this, new ColumnEventArgs(item.Column));
            } else {
                this.ScrollPanel.ResumeLayout();
            }
        }

        private void NavigatingColumnView_SubColumnAdded(object sender, ColumnEventArgs e) {
            ItemsColumn column = e.Column as ItemsColumn;
            column.LoadItems();
            this.ScrollToLastColumn();
        }
    }
}
