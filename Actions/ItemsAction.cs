using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Controls;

namespace Trail.Actions {
    public abstract class ItemsAction : ActionProgressControl {
        BackgroundWorker worker;

        public event RunWorkerCompletedEventHandler Completed;

        public void Start() {
            if (worker != null && worker.IsBusy) return;

            worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker.DoWork += DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        public void Cancel() {
            worker.CancelAsync();
        }

        public abstract void DoWork(object sender, DoWorkEventArgs e);

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            this.Progress = e.ProgressPercentage;
            this.DescriptionText = e.UserState.ToString();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (Completed != null) Completed(this, e);
        }
    }
}
