using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trail.Actions {
    public class FilesCopyAction : ItemsAction {
        private Queue<Tuple<string, string>> queue = new Queue<Tuple<string, string>>();
        private CancellationTokenSource cts = new CancellationTokenSource();

        public string[] Items { get; private set; }
        public DirectoryInfo Destination { get; private set; }
        public int Count { get; private set; }

        public FilesCopyAction(string[] items, DirectoryInfo destination) {
            this.Items = items;
            this.Destination = destination;
            this.HeaderText = "Copy files to \"" + destination.Name + "\"...";

            this.CancelButtonClicked += FilesCopyAction_CancelButtonClicked;
        }

        private void FilesCopyAction_CancelButtonClicked(object sender, EventArgs e) {
            cts.Cancel();
        }

        private void enqueueItem(string oldPath, string newPath, DoWorkEventArgs e) {
            if (e.Cancel) return;

            if (File.Exists(oldPath)) {
                queue.Enqueue(new Tuple<string, string>(oldPath, newPath));
            } else if (Directory.Exists(oldPath)) {
                if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

                foreach (string p in Directory.GetFiles(oldPath)) {
                    if (e.Cancel) return;

                    enqueueItem(p, Path.Combine(newPath, new FileInfo(p).Name), e);
                }

                foreach (string p in Directory.GetDirectories(oldPath)) {
                    if (e.Cancel) return;

                    string pp = Path.Combine(newPath, new DirectoryInfo(p).Name);
                    enqueueItem(p, pp, e);
                    if (!Directory.Exists(pp)) Directory.CreateDirectory(pp);
                }
            }
        }

        public override void DoWork(BackgroundWorker sender, DoWorkEventArgs e) {
            sender.ReportProgress(0, "Preparing...");

            foreach (string item in Items) {
                if (e.Cancel) return;

                if (File.Exists(item)) {
                    enqueueItem(item, Path.Combine(Destination.FullName, new FileInfo(item).Name), e);
                } else if (Directory.Exists(item)) {
                    enqueueItem(item, Path.Combine(Destination.FullName, new DirectoryInfo(item).Name), e);
                }
            }

            this.Count = queue.Count;

            while (queue.Count > 0) {
                if (e.Cancel) return;

                Tuple<string, string> order = queue.Dequeue();
                int percentage = (this.Count - queue.Count - 1) * 100 / this.Count;
                int endPercentage = (this.Count - queue.Count) * 100 / this.Count;

                sender.ReportProgress(percentage, "Copy \"" + new FileInfo(order.Item1).Name + "\"...");

                try {
                    Mischel.IO.FileUtil.CopyFile(order.Item1, order.Item2, (evt) => {
                        double p = (double)evt.TotalBytesTransferred / evt.TotalFileSize;
                        sender.ReportProgress(percentage + (int)((endPercentage - percentage) * p), null);
                    }, IntPtr.Zero, Mischel.IO.CopyFileOptions.None, cts.Token);
                } catch (IOException ex) {
                    if (ex.HResult == 1235) e.Cancel = true;
                    else throw ex;
                }
            }
        }
    }
}
