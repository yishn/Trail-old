using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Actions;
using Trail.Controls;
using Trail.DataTypes;
using Trail.Fx;
using Trail.Templates;

namespace Trail.Modules {
    public class ActionControl : ProgressControl {
        private IntAnimation progressAnimation = new IntAnimation();
        private CancellationTokenSource cancellation;

        public ItemsAction Action { get; set; }
        public bool IsBusy { get; private set; }

        public event EventHandler Completed;

        public ActionControl(ItemsAction action) {
            this.Action = action;
            this.HeaderText = action.HeaderText ?? "Action";
            this.DescriptionText = "Waiting in queue...";

            this.CancelButtonClicked += ItemsAction_CancelButtonClicked;
        }

        public async void Start() {
            if (this.IsBusy) return;
            this.IsBusy = true;

            IProgress<Tuple<int, string>> progress = new Progress<Tuple<int, string>>(updateProgress);
            cancellation = new CancellationTokenSource();

            await Task.Run(() => {
                try {
                    Control.CheckForIllegalCrossThreadCalls = false;
                    this.Action.DoWork(progress, cancellation.Token);
                    Control.CheckForIllegalCrossThreadCalls = true;
                    progress.Report(new Tuple<int, string>(100, "Completed."));
                } catch (OperationCanceledException) {
                    progress.Report(new Tuple<int, string>(100, "Cancelled."));
                } catch (ShowErrorException e) {
                    progress.Report(new Tuple<int, string>(0, e.Message));
                } catch {
                    progress.Report(new Tuple<int, string>(0, "An error occurred."));
                }

                Thread.Sleep(1000);
            });

            this.IsBusy = false;
            if (Completed != null) Completed(this, EventArgs.Empty);
        }

        public void Cancel() {
            if (cancellation != null) cancellation.Cancel();
        }

        private void updateProgress(Tuple<int, string> t) {
            if (!progressAnimation.Enabled || t.Item1 == 100 || t.Item1 == 0) {
                progressAnimation.Stop();
                progressAnimation = new IntAnimation();
                progressAnimation.Start(this.Progress, t.Item1).Tick += (_, value) => {
                    this.Progress = value;
                };
            }

            if (t.Item2 != null) this.DescriptionText = t.Item2;
        }

        private void ItemsAction_CancelButtonClicked(object sender, EventArgs e) {
            Cancel();
        }
    }
}
