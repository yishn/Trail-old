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
using Trail.Modules;
using Trail.Helpers;
using System.Threading;

namespace Trail.Columns {
    public class DirectoryColumn : ItemsColumn {
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public DirectoryInfo Directory { get; private set; }

        public DirectoryColumn(string itemsPath) : this(new DirectoryInfo(itemsPath)) { }
        public DirectoryColumn(DirectoryInfo directory) : base(directory.FullName) {
            this.Directory = directory;
            this.ListViewControl.AllowDrop = true;

            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = false;
            watcher.SynchronizingObject = ListViewControl;

            this.ItemActivate += DirectoryColumn_ItemActivate;
            this.ListViewControl.ItemDrag += ListViewControl_ItemDrag;

            watcher.Created += watcher_Created;
            watcher.Deleted += watcher_Deleted;
            watcher.Renamed += watcher_Renamed;
        }

        #region Drag & Drop

        private void ListViewControl_ItemDrag(object sender, ItemDragEventArgs e) {
            string[] files = new string[ListViewControl.SelectedItems.Count];
            int i = 0;

            foreach (ColumnListViewItem item in ListViewControl.SelectedItems) {
                string path;
                if (item.Tag is DirectoryInfo) path = (item.Tag as DirectoryInfo).FullName;
                else path = (item.Tag as FileInfo).FullName;

                files[i++] = path;
            }

            ListViewControl.DoDragDrop(new DataObject(DataFormats.FileDrop, files), DragDropEffects.Copy);
        }

        #endregion

        #region FileSystemWatcher methods

        private void watcher_Deleted(object sender, FileSystemEventArgs e) {
            foreach (ColumnListViewItem item in ListViewControl.Items) {
                if (item.Text != e.Name) continue;
                item.Remove();
                break;
            }
        }

        private void watcher_Created(object sender, FileSystemEventArgs e) {
            bool isDir = !File.Exists(e.FullPath);

            ColumnListViewItem item = new ColumnListViewItem() {
                Text = e.Name,
                Tag = isDir ? new DirectoryInfo(e.FullPath) as object : new FileInfo(e.FullPath) as object
            };
            item.ImageKey = getImageKey(item);
            if (isDir) item.SubColumn = new DirectoryColumn(item.Tag as DirectoryInfo);

            ListViewControl.Items.Add(item);
            ListViewControl.Sort();

            OnLoadingCompleted();
        }

        private void watcher_Renamed(object sender, RenamedEventArgs e) {
            watcher_Deleted(sender, new FileSystemEventArgs(WatcherChangeTypes.Deleted, Directory.FullName, e.OldName));
            watcher_Created(sender, new FileSystemEventArgs(WatcherChangeTypes.Created, Directory.FullName, e.Name));
        }

        #endregion

        private void DirectoryColumn_ItemActivate(object sender, ColumnListViewItem item) {
            if (!(item.Tag is FileInfo) || item.SubColumn != null) return;

            ProcessStartInfo info = new ProcessStartInfo((item.Tag as FileInfo).FullName);
            info.WorkingDirectory = (item.Tag as FileInfo).DirectoryName;
            Process.Start(info);
        }

        private string getImageKey(ColumnListViewItem item) {
            if (item.Tag is DirectoryInfo) return ".folder";

            FileInfo fI = item.Tag as FileInfo;
            string ext = Path.GetExtension(fI.Name).Replace(".", "");

            if (ext == "") return fI.FullName;

            List<string> patterns = Persistence.GetPreferenceList("directorycolumn.individual_icon_files");
            if (patterns.Any(x => fI.Name.MatchesPattern(x))) return fI.FullName;

            return ext;
        }

        protected override List<ColumnListViewItem> loadData(CancellationToken token) {
            List<ColumnListViewItem> result = new List<ColumnListViewItem>();
            List<string> patterns = Persistence.GetPreferenceList("directorycolumn.directory_exclude_patterns");

            foreach (DirectoryInfo dI in this.Directory.GetDirectories()) {
                token.ThrowIfCancellationRequested();
                if (patterns.Any(x => dI.FullName.MatchesPattern(x))) continue;

                result.Add(new ColumnListViewItem() {
                    SubColumn = new DirectoryColumn(dI),
                    Text = dI.Name,
                    Tag = dI,
                    ImageKey = ".folder"
                });
            }

            patterns = Persistence.GetPreferenceList("directorycolumn.file_exclude_patterns");

            foreach (FileInfo fI in this.Directory.GetFiles()) {
                token.ThrowIfCancellationRequested();
                if (patterns.Any(x => fI.FullName.MatchesPattern(x))) continue;
  
                ColumnListViewItem item = new ColumnListViewItem() {
                    Text = fI.Name,
                    Tag = fI,
                };
                item.ImageKey = getImageKey(item);
                result.Add(item);
            }

            watcher.Path = Directory.FullName;
            watcher.EnableRaisingEvents = true;

            return result;
        }

        public override string GetHeaderText() {
            if (Directory.Root.FullName == Directory.FullName) {
                DriveInfo drive = new DriveInfo(Directory.FullName);
                if (!drive.IsReady || drive.VolumeLabel.Trim() == "") return Directory.Name;
                return drive.VolumeLabel + " (" + Directory.Name.Replace(Path.DirectorySeparatorChar, ')');
            }
            
            return Directory.Name;
        }

        public override Image GetIcon(ColumnListViewItem item) {
            if (item.Tag is DirectoryInfo) return base.GetIcon(item);
            FileInfo fI = item.Tag as FileInfo;
            return Etier.IconHelper.IconReader.GetFileIcon(fI.FullName, Etier.IconHelper.IconReader.IconSize.Small, false).ToBitmap();
        }

        public override List<ItemsColumn> GetTrail() {
            if (!Directory.Exists) return base.GetTrail();

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
