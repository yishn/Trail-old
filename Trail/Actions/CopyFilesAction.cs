using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Trail.Controls;
using Trail.DataTypes;
using Trail.Templates;

namespace Trail.Actions {
    public class CopyFilesAction : ItemsAction {
        private Queue<Tuple<string, string>> queue = new Queue<Tuple<string, string>>();

        public string[] Items { get; private set; }
        public DirectoryInfo Destination { get; private set; }
        public int Count { get; private set; }

        public CopyFilesAction(string[] items, DirectoryInfo destination, IHost host) : base(host) {
            this.HeaderText = "Copy files to \"" + destination.Name + "\"";
            this.Items = items;
            this.Destination = destination;
        }

        private void enqueueItem(string oldPath, string newPath, CancellationToken token) {
            if (File.Exists(oldPath)) {
                if (new FileInfo(oldPath).Directory.FullName == new FileInfo(newPath).Directory.FullName) return;
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
            ChoiceDialog dialog = new ChoiceDialog("Copy Items", "Replace &All", "&Replace This Item", "&Skip Item", "&Cancel");

            foreach (string item in Items) {
                token.ThrowIfCancellationRequested();

                string name = "", newItem = "";
                bool isDir = false, exists = false, newItemFileExists = false, newItemDirectoryExists = false;

                if (File.Exists(item)) {
                    exists = true;
                    name = new FileInfo(item).Name;
                    newItem = Path.Combine(Destination.FullName, name);
                    isDir = false;
                } else if (Directory.Exists(item)) {
                    exists = true;
                    name = new DirectoryInfo(item).Name;
                    newItem = Path.Combine(Destination.FullName, name);
                    isDir = true;
                }

                if (!exists) continue;

                newItemFileExists = File.Exists(newItem);
                newItemDirectoryExists = Directory.Exists(newItem);

                if (!isDir && newItemDirectoryExists || isDir && newItemFileExists) {
                    // Something went really wrong, do something!
                    continue;
                }

                if (item == newItem) continue;

                if ((newItemFileExists || newItemDirectoryExists) && !yesToAll) {
                    dialog.Owner = Host.MainForm;
                    DialogResult result = dialog.ShowDialog(
                        "There is already a " 
                        + (newItemFileExists ? "file" : "directory") 
                        + " named \"" + name + "\" at the destination."
                    );

                    if (result == DialogResult.No) continue;
                    else if (result == DialogResult.Cancel) throw new OperationCanceledException();
                    else if (result == DialogResult.OK) yesToAll = true;
                }

                enqueueItem(item, newItem, token);
            }
        }

        public override void DoWork(IProgress<Tuple<int, string>> progress, CancellationToken token) {
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
