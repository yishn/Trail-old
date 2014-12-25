using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trail.Controls;
using Trail.Fx;

namespace Trail.Actions {
    public abstract class ItemsAction : ActionProgressControl {
        private IntAnimation progressAnimation = new IntAnimation();
        private BackgroundWorker worker;

        public event RunWorkerCompletedEventHandler Completed;

        public ItemsAction() {
            this.CancelButtonClicked += ItemsAction_CancelButtonClicked;
        }

        public void Start() {
            if (worker != null && worker.IsBusy) return;

            worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        public void Cancel() {
            worker.CancelAsync();
        }

        public abstract void DoWork(BackgroundWorker sender, DoWorkEventArgs e);

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            try {
                DoWork(sender as BackgroundWorker, e);
                if (!e.Cancel) worker.ReportProgress(100, "Completed.");
                else worker.ReportProgress(100, "Cancelled.");
            } catch (Exception ex) {
                e.Result = ex;
                worker.ReportProgress(0, "An error occurred.");
            }

            Thread.Sleep(1000);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            if (!progressAnimation.Enabled || e.ProgressPercentage == 100 || e.ProgressPercentage == 0) {
                progressAnimation = new IntAnimation();
                progressAnimation.Start(this.Progress, e.ProgressPercentage).Tick += (_, value) => {
                    this.Progress = value;
                };
            }

            if (e.UserState != null)
                this.DescriptionText = e.UserState.ToString();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (Completed != null) Completed(this, e);
        }

        private void ItemsAction_CancelButtonClicked(object sender, EventArgs e) {
            this.Cancel();
        }
    }
}
