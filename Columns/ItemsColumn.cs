using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;
using Trail.DataTypes;
using Trail.Modules;

namespace Trail.Columns {
    public abstract class ItemsColumn : ColumnControl {
        private BackgroundWorker _worker;

        public string ItemsPath { get; private set; }

        public event RunWorkerCompletedEventHandler LoadingCompleted;

        public ItemsColumn(string itemsPath) {
            this.ItemsPath = itemsPath;
            this.ListViewControl.ItemActivate += ListViewControl_ItemActivate;
            this.ListViewControl.ListViewItemSorter = new ItemsColumnListComparer();
        }

        private void ListViewControl_ItemActivate(object sender, EventArgs e) {
            ItemActivated(ListViewControl.SelectedItems[0] as ColumnListViewItem);
        }

        protected abstract List<ColumnListViewItem> loadData(DoWorkEventArgs e);
        public abstract void ItemActivated(ColumnListViewItem item);
        public abstract ItemsColumn Duplicate();

        public virtual List<ItemsColumn> GetTrail() {
            return new List<ItemsColumn>(new ItemsColumn[] { this });
        }

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

        public ColumnData GetColumnData() {
            return new ColumnData(this.GetType().FullName, this.Path);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) return;
            List<ColumnListViewItem> result = e.Result as List<ColumnListViewItem>;

            ListViewControl.Items.Clear();
            ListViewControl.Items.AddRange(result.ToArray());
            ListViewControl.Sort();
            UpdateColumnWidth();

            if (LoadingCompleted != null) LoadingCompleted(this, e);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            e.Result = loadData(e);
        }

        protected virtual void OnLoadingCompleted(RunWorkerCompletedEventArgs e) {
            if (LoadingCompleted != null) LoadingCompleted(this, e);
        }
    }
}
