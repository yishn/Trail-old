using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;

namespace Trail.Actions {
    public class FilesCopyAction : IAction {
        private Queue<Tuple<string, string>> queue = new Queue<Tuple<string, string>>();

        public string HeaderText { get { return "Copy files to \"" + Destination.Name + "\""; } }
        public string[] Items { get; private set; }
        public DirectoryInfo Destination { get; private set; }
        public int Count { get; private set; }

        public FilesCopyAction(string[] items, DirectoryInfo destination) {
            this.Items = items;
            this.Destination = destination;
        }

        private void enqueueItem(string oldPath, string newPath, CancellationToken token) {
            if (File.Exists(oldPath)) {
                queue.Enqueue(new Tuple<string, string>(oldPath, newPath));
            } else if (Directory.Exists(oldPath)) {
                foreach (string p in Directory.GetFiles(oldPath)) {
                    token.ThrowIfCancellationRequested();
                    enqueueItem(p, Path.Combine(newPath, new FileInfo(p).Name), token);
                }

                foreach (string p in Directory.GetDirectories(oldPath)) {
                    token.ThrowIfCancellationRequested();
                    enqueueItem(p, Path.Combine(newPath, new DirectoryInfo(p).Name), token);
                }
            }
        }

        private void preparation(CancellationToken token) {
            bool yesToAll = false;
            ChoiceDialog dialog = new ChoiceDialog("Copy File", "Replace &All", "&Replace This File", "&Skip File", "&Cancel");

            foreach (string item in Items) {
                token.ThrowIfCancellationRequested();

                if (File.Exists(item)) {
                    string name = new FileInfo(item).Name;
                    string newItem = Path.Combine(Destination.FullName, name);

                    if (File.Exists(newItem) && !yesToAll) {
                        DialogResult result = dialog.ShowDialog(
                            "There is already a file named \"" + name + "\" at the destination."
                        );

                        if (result == DialogResult.No) continue;
                        else if (result == DialogResult.Cancel) return;
                        else if (result == DialogResult.OK) yesToAll = true;
                    }

                    enqueueItem(item, newItem, token);
                } else if (Directory.Exists(item)) {
                    string name = new DirectoryInfo(item).Name;
                    string newItem = Path.Combine(Destination.FullName, name);

                    if (Directory.Exists(newItem) && !yesToAll) {
                        DialogResult result = dialog.ShowDialog(
                            "There is already a directory named \"" + name + "\" at the destination."
                        );

                        if (result == DialogResult.No) continue;
                        else if (result == DialogResult.Cancel) return;
                        else if (result == DialogResult.OK) yesToAll = true;
                    }

                    enqueueItem(item, newItem, token);
                }
            }
        }

        public void DoWork(IProgress<Tuple<int, string>> progress, CancellationToken token) {
            progress.Report(new Tuple<int, string>(0, "Preparing..."));
            preparation(token);
            this.Count = queue.Count;

            while (queue.Count > 0) {
                token.ThrowIfCancellationRequested();

                Tuple<string, string> order = queue.Dequeue();
                int percentage = (this.Count - queue.Count - 1) * 100 / this.Count;
                int endPercentage = (this.Count - queue.Count) * 100 / this.Count;

                progress.Report(new Tuple<int, string>(percentage, new FileInfo(order.Item1).Name));

                try {
                    FileInfo file = new FileInfo(order.Item2);
                    if (!file.Directory.Exists) file.Directory.Create();

                    Mischel.IO.FileUtil.CopyFile(order.Item1, order.Item2, (status) => {
                        double p = (double)status.TotalBytesTransferred / status.TotalFileSize;
                        progress.Report(new Tuple<int, string>(percentage + (int)((endPercentage - percentage) * p), null));
                    }, IntPtr.Zero, Mischel.IO.CopyFileOptions.None, token);
                } catch (IOException ex) {
                    if (ex.HResult == 1235) throw new OperationCanceledException();
                    else throw ex;
                }
            }
        }
    }
}
