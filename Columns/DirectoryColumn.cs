using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;

namespace Trail.Columns {
    public class DirectoryColumn : ItemsColumn {
        private FileSystemWatcher _watcher;

        public DirectoryInfo Directory { get; private set; }

        public DirectoryColumn(string itemsPath) : this(new DirectoryInfo(itemsPath)) { }
        public DirectoryColumn(DirectoryInfo directory) : base(directory.FullName) {
            _watcher = new FileSystemWatcher(directory.FullName) {
                IncludeSubdirectories = false,
                EnableRaisingEvents = false,
                SynchronizingObject = ListViewControl
            };
            _watcher.Created += _watcher_Created;
            _watcher.Deleted += _watcher_Deleted;
            _watcher.Renamed += _watcher_Renamed;

            this.Directory = directory;
            this.HeaderText = directory.Name;
        }

        #region FileSystemWatcher methods

        private void _watcher_Deleted(object sender, FileSystemEventArgs e) {
            foreach (ColumnListViewItem item in ListViewControl.Items) {
                if (item.Text != e.Name) continue;
                item.Remove();
                break;
            }
        }

        private void _watcher_Created(object sender, FileSystemEventArgs e) {
            bool isDir = !File.Exists(e.FullPath);

            ColumnListViewItem item = new ColumnListViewItem() {
                Text = e.Name,
                Tag = isDir ? new DirectoryInfo(e.FullPath) as object : new FileInfo(e.FullPath) as object
            };
            item.ImageKey = getImageKey(item);
            if (isDir) item.SubColumn = new DirectoryColumn(item.Tag as DirectoryInfo);

            ListViewControl.Items.Add(item);
            ListViewControl.Sort();

            OnLoadingCompleted(new RunWorkerCompletedEventArgs(null, null, false));
        }

        private void _watcher_Renamed(object sender, RenamedEventArgs e) {
            _watcher_Deleted(sender, new FileSystemEventArgs(WatcherChangeTypes.Deleted, Directory.FullName, e.OldName));
            _watcher_Created(sender, new FileSystemEventArgs(WatcherChangeTypes.Created, Directory.FullName, e.Name));
        }

        #endregion

        private string getImageKey(ColumnListViewItem item) {
            if (item.Tag is DirectoryInfo) return ".folder";

            FileInfo fI = item.Tag as FileInfo;
            string ext = Path.GetExtension(fI.Name);

            if (ext == "" || ext == ".exe" || ext == ".lnk" || ext == ".ico") return fI.FullName;
            return ext;
        }

        public override List<ColumnListViewItem> LoadData(DoWorkEventArgs e) {
            _watcher.EnableRaisingEvents = true;
            List<ColumnListViewItem> result = new List<ColumnListViewItem>();

            foreach (DirectoryInfo dI in this.Directory.GetDirectories()) {
                if (e.Cancel) return null;

                result.Add(new ColumnListViewItem() {
                    SubColumn = new DirectoryColumn(dI),
                    Text = dI.Name,
                    Tag = dI,
                    ImageKey = ".folder"
                });
            }

            foreach (FileInfo fI in this.Directory.GetFiles()) {
                if (e.Cancel) return null;
                string ext = Path.GetExtension(fI.FullName);
                
                ColumnListViewItem item = new ColumnListViewItem() {
                    Text = fI.Name,
                    Tag = fI,
                };
                item.ImageKey = getImageKey(item);
                result.Add(item);
            }

            return result;
        }

        public override void ItemActivated(ColumnListViewItem item) {
            if (!(item.Tag is FileInfo)) return;

            ProcessStartInfo info = new ProcessStartInfo((item.Tag as FileInfo).FullName);
            info.WorkingDirectory = (item.Tag as FileInfo).DirectoryName;
            Process.Start(info);
        }

        public override Image GetIcon(ColumnListViewItem item) {
            if (item.Tag is DirectoryInfo) return base.GetIcon(item);
            FileInfo fI = item.Tag as FileInfo;
            return Etier.IconHelper.IconReader.GetFileIcon(fI.FullName, Etier.IconHelper.IconReader.IconSize.Small, false).ToBitmap();
        }

        public override List<ItemsColumn> GetTrail() {
            DirectoryInfo current = this.Directory;
            List<ItemsColumn> trail = new List<ItemsColumn>();
            trail.Add(this);

            while (current.Root.FullName != current.FullName) {
                current = current.Parent;
                trail.Add(new DirectoryColumn(current));
            }

            trail.Reverse();
            return trail;
        }

        public override ItemsColumn Duplicate() {
            return new DirectoryColumn(this.Directory);
        }
    }
}
