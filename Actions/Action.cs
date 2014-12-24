using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Controls;

namespace Trail.Actions {
    public abstract class Action : ActionProgressControl {
        BackgroundWorker worker;

        public void Start() {
            if (worker != null && worker.IsBusy) return;

            worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker.DoWork += DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        public abstract void DoWork(object sender, DoWorkEventArgs e);

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            this.Progress = e.ProgressPercentage;
            this.DescriptionText = e.UserState.ToString();
        }
    }
}
