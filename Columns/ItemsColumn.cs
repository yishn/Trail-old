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
        private BackgroundWorker worker;

        public string ItemsPath { get; private set; }

        public event RunWorkerCompletedEventHandler LoadingCompleted;

        public ItemsColumn(string itemsPath) {
            this.ItemsPath = itemsPath;
            this.HeaderText = "";

            this.ListViewControl.ItemActivate += ListViewControl_ItemActivate;
            this.ListViewControl.ListViewItemSorter = new ItemsColumnListComparer();
        }

        private void ListViewControl_ItemActivate(object sender, EventArgs e) {
            ItemActivated(ListViewControl.SelectedItems[0] as ColumnListViewItem);
        }

        protected abstract List<ColumnListViewItem> loadData(DoWorkEventArgs e);
        public abstract string GetHeaderText();
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
            if (worker != null && worker.IsBusy) return;

            worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        public ColumnData GetColumnData() {
            return new ColumnData(this.GetType().FullName, this.ItemsPath);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) return;
            if (e.Result is Exception) {
                Exception result = e.Result as Exception;

                this.ShowError = true;

                if (result is UnauthorizedAccessException) {
                    this.ErrorText = "Access denied.";
                } else {
                    this.ErrorText = result.Message;
                }
                
            } else if (e.Result is List<ColumnListViewItem>) {
                List<ColumnListViewItem> result = e.Result as List<ColumnListViewItem>;

                this.ShowError = false;
                this.HeaderText = GetHeaderText();

                ListViewControl.Items.Clear();
                ListViewControl.Items.AddRange(result.ToArray());
                ListViewControl.Sort();
                UpdateColumnWidth();

                if (LoadingCompleted != null) LoadingCompleted(this, e);
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            try {
                e.Result = loadData(e);
            } catch (Exception ex) {
                e.Result = ex;
            }
        }

        protected virtual void OnLoadingCompleted(RunWorkerCompletedEventArgs e) {
            if (LoadingCompleted != null) LoadingCompleted(this, e);
        }
    }
}
