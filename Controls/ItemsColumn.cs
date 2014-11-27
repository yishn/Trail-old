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
        }

        public abstract List<ListViewItem> LoadData();

        public void RefreshItems() {
            if (worker != null) worker.CancelAsync();

            worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) return;

            List<ListViewItem> result = e.Result as List<ListViewItem>;
            this.Items.Clear();
            this.Items.AddRange(result.ToArray());
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            e.Result = LoadData();
        }
    }
}
