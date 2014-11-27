using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public abstract class ItemsColumn : ColumnControl {
        private BackgroundWorker worker;

        public ItemsColumn() {
            this.RefreshItems();
            this.ListViewControl.ColumnClick += ListViewControl_ColumnClick;
        }

        private void ListViewControl_ColumnClick(object sender, ColumnClickEventArgs e) {
            if (ListViewControl.Tag == null) return;

            List<ListViewItem> items = ListViewControl.Tag as List<ListViewItem>;
            items.Reverse();

            ListViewControl.Items.Clear();
            ListViewControl.Items.AddRange(items.ToArray());
        }

        public abstract List<ListViewItem> LoadData();
        public abstract int Compare(ListViewItem item1, ListViewItem item2);

        public void RefreshItems() {
            if (worker != null) worker.CancelAsync();

            worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) return;

            List<ListViewItem> result = e.Result as List<ListViewItem>;
            result.Sort((l1, l2) => Compare(l1, l2));
            ListViewControl.Tag = result;

            ListViewControl.Items.Clear();
            ListViewControl.Items.AddRange(result.ToArray());
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            e.Result = LoadData();
        }
    }
}
