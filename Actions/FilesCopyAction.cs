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

        private void enqueueItem(string oldPath, string newPath) {
            if (File.Exists(oldPath)) {
                queue.Enqueue(new Tuple<string, string>(oldPath, newPath));
            } else if (Directory.Exists(oldPath)) {
                foreach (string path in Directory.GetFiles(oldPath)) {
                    enqueueItem(path, Path.Combine(newPath, new FileInfo(path).Name));
                }
                foreach (string path in Directory.GetDirectories(oldPath)) {
                    enqueueItem(path, Path.Combine(newPath, new DirectoryInfo(path).Name));
                }
            }
        }

        public override void DoWork(BackgroundWorker sender, DoWorkEventArgs e) {
            sender.ReportProgress(0, "Counting files...");

            foreach (string item in Items) {
                if (File.Exists(item)) {
                    enqueueItem(item, Path.Combine(Destination.FullName, new FileInfo(item).Name));
                } else if (Directory.Exists(item)) {
                    enqueueItem(item, Path.Combine(Destination.FullName, new DirectoryInfo(item).Name));
                }
            }

            this.Count = queue.Count;

            while (queue.Count > 0) {
                Tuple<string, string> order = queue.Dequeue();
                int percentage = (this.Count - queue.Count - 1) * 100 / this.Count;
                int endPercentage = (this.Count - queue.Count) * 100 / this.Count;

                sender.ReportProgress(percentage, "Copy \"" + new FileInfo(order.Item1).Name + "\"...");

                Mischel.IO.FileUtil.CopyFile(order.Item1, order.Item2, (evt) => {
                    double p = (double)evt.TotalBytesTransferred / evt.TotalFileSize;
                    sender.ReportProgress(percentage + (int)((endPercentage - percentage) * p), null);
                }, IntPtr.Zero, Mischel.IO.CopyFileOptions.None, cts.Token);
            }
        }
    }
}
