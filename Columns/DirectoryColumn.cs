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

        public DirectoryInfo Directory { get; set; }

        public event EventHandler<ColumnListViewItem> WatcherObserved;

        public DirectoryColumn(DirectoryInfo directory) : base() {
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
                if (WatcherObserved != null) WatcherObserved(this, null);
                break;
            }
        }

        private void _watcher_Created(object sender, FileSystemEventArgs e) {
            bool isDir = !File.Exists(e.FullPath);

            ColumnListViewItem item = new ColumnListViewItem() {
                Text = e.Name,
                ImageKey = isDir ? ".folder" : ".file"
            };

            ListViewControl.Items.Add(item);
            ListViewControl.Sort();

            if (WatcherObserved != null) WatcherObserved(this, item);
        }

        private void _watcher_Renamed(object sender, RenamedEventArgs e) {
            foreach (ColumnListViewItem item in ListViewControl.Items) {
                if (item.Text != e.Name) continue;
                item.Text = e.Name;
                item.Tag = File.Exists(e.FullPath) ? new FileInfo(e.FullPath) as object : new DirectoryInfo(e.FullPath) as object;

                if (WatcherObserved != null) WatcherObserved(this, item);
                break;
            }
        }

        #endregion

        public override List<ColumnListViewItem> LoadData(DoWorkEventArgs e) {
            _watcher.EnableRaisingEvents = true;
            List<ColumnListViewItem> result = new List<ColumnListViewItem>();

            foreach (DirectoryInfo dI in this.Directory.GetDirectories()) {
                if (e.Cancel) return null;

                result.Add(new ColumnListViewItem() {
                    Column = new DirectoryColumn(dI),
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
                    ImageKey = ext == "" ? ".file" : ext
                };
                if (ext == ".exe" || ext == ".lnk") item.ImageKey = fI.FullName;
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
            string ext = Path.GetExtension(fI.Name);
            
            if (ext != ".exe" && ext != ".lnk") return base.GetIcon(item);
            return Etier.IconHelper.IconReader.GetFileIcon(fI.FullName, Etier.IconHelper.IconReader.IconSize.Small, false).ToBitmap();
        }
    }
}
