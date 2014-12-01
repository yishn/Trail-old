using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;

namespace Trail.Columns {
    public abstract class ItemsColumn : ColumnControl {
        private BackgroundWorker _worker;

        public event RunWorkerCompletedEventHandler LoadingCompleted;

        public ItemsColumn() {
            this.ListViewControl.ColumnClick += ListViewControl_ColumnClick;
            this.ListViewControl.ItemActivate += ListViewControl_ItemActivate;
        }

        private void ListViewControl_ColumnClick(object sender, ColumnClickEventArgs e) {
            if (ListViewControl.Tag == null) return;

            List<ColumnListViewItem> items = ListViewControl.Tag as List<ColumnListViewItem>;
            items.Reverse();

            ListViewControl.Items.Clear();
            ListViewControl.Items.AddRange(items.ToArray());
            UpdateColumnWidth();
        }

        private void ListViewControl_ItemActivate(object sender, EventArgs e) {
            ItemActivated(ListViewControl.SelectedItems[0] as ColumnListViewItem);
        }

        public abstract List<ColumnListViewItem> LoadData(DoWorkEventArgs e);
        public abstract int Compare(ColumnListViewItem item1, ColumnListViewItem item2);
        public abstract void ItemActivated(ColumnListViewItem item);

        public virtual Image GetIcon(ColumnListViewItem item) {
            Icon i = Etier.IconHelper.IconReader.GetFileIcon(item.Text, Etier.IconHelper.IconReader.IconSize.Small, false);
            return i.ToBitmap();
        }

        public void LoadItems() {
            if (_worker != null && _worker.IsBusy) return;

            _worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) return;

            List<ColumnListViewItem> result = e.Result as List<ColumnListViewItem>;
            result.Sort((l1, l2) => Compare(l1, l2));
            ListViewControl.Tag = result;

            ListViewControl.Items.Clear();
            ListViewControl.Items.AddRange(result.ToArray());
            UpdateColumnWidth();

            if (LoadingCompleted != null) LoadingCompleted(this, e);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            e.Result = LoadData(e);
        }
    }
}
