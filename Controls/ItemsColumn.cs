using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public abstract class ItemsColumn : ColumnControl {
        private BackgroundWorker worker;

        public event RunWorkerCompletedEventHandler LoadingCompleted;

        public ItemsColumn() {
            this.ListViewControl.ColumnClick += ListViewControl_ColumnClick;
            this.ListViewControl.ItemActivate += ListViewControl_ItemActivate;
        }

        private void ListViewControl_ColumnClick(object sender, ColumnClickEventArgs e) {
            if (ListViewControl.Tag == null) return;

            List<ColumnItem> items = ListViewControl.Tag as List<ColumnItem>;
            items.Reverse();

            ListViewControl.Items.Clear();
            ListViewControl.Items.AddRange(items.ToArray());
            UpdateColumnWidth();
        }

        private void ListViewControl_ItemActivate(object sender, EventArgs e) {
            ItemActivated(ListViewControl.SelectedItems[0] as ColumnItem);
        }

        public abstract List<ColumnItem> LoadData(DoWorkEventArgs e);
        public abstract int Compare(ColumnItem item1, ColumnItem item2);
        public abstract void ItemActivated(ColumnItem item);
        public abstract Image GetIcon(ColumnItem item);

        public void RefreshItems() {
            if (worker != null && worker.IsBusy) return;

            worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        public void UpdateColumnWidth() {
            this.SuspendLayout();
            ListViewControl.BeginUpdate();
            ListViewControl.Width++;
            ListViewControl.Width--;
            ListViewControl.EndUpdate();
            this.ResumeLayout();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) return;

            List<ColumnItem> result = e.Result as List<ColumnItem>;
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
