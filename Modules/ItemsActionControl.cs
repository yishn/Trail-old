using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trail.Actions;
using Trail.Controls;
using Trail.Fx;

namespace Trail.Modules {
    public class ItemsActionControl : ActionProgressControl {
        private IntAnimation progressAnimation = new IntAnimation();
        private CancellationTokenSource cancellation = new CancellationTokenSource();

        public IAction Action { get; set; }

        public event EventHandler Completed;

        public ItemsActionControl(IAction action) {
            this.Action = action;
            this.HeaderText = action.HeaderText;
            this.DescriptionText = "Waiting in queue...";

            this.CancelButtonClicked += ItemsAction_CancelButtonClicked;
        }

        public async void Start() {
            IProgress<Tuple<int, string>> progress = new Progress<Tuple<int, string>>(t => {
                if (!progressAnimation.Enabled || t.Item1 == 100 || t.Item1 == 0) {
                    progressAnimation.Stop();
                    progressAnimation = new IntAnimation();
                    progressAnimation.Start(this.Progress, t.Item1).Tick += (_, value) => {
                        this.Progress = value;
                    };
                }

                if (t.Item2 != null) this.DescriptionText = t.Item2;
            });

            await Task.Run(() => {
                try {
                    this.Action.DoWork(progress, cancellation.Token);
                    progress.Report(new Tuple<int, string>(100, "Completed."));
                } catch (OperationCanceledException) {
                    progress.Report(new Tuple<int, string>(100, "Cancelled."));
                } catch (Exception) {
                    progress.Report(new Tuple<int, string>(0, "An error occurred."));
                }

                Thread.Sleep(1000);
            });

            if (Completed != null) Completed(this, EventArgs.Empty);
        }

        public void Cancel() {
            cancellation.Cancel();
        }

        private void ItemsAction_CancelButtonClicked(object sender, EventArgs e) {
            Cancel();
        }
    }
}
