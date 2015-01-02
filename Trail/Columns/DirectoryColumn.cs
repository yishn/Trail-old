﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Trail.Helpers;
using Trail.DataTypes;
using Trail.Actions;
using Trail.Controls;
using Trail.Templates;

namespace Trail.Columns {
    public class DirectoryColumn : ItemsColumn {
        private FileSystemWatcher watcher = new FileSystemWatcher();
        private ContextMenuStrip contextMenu;

        public DirectoryInfo DirectoryData { get; private set; }

        public DirectoryColumn(string itemsPath, IHost host) : this(new DirectoryInfo(itemsPath), host) { }
        public DirectoryColumn(DirectoryInfo directory, IHost host) : base(directory.FullName, host) {
            initializeContextMenu();
            this.DirectoryData = directory;

            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = false;
            watcher.SynchronizingObject = ListViewControl;

            this.ListViewControl.ItemDrag += ListViewControl_ItemDrag;
            this.ListViewControl.DragEnter += ListViewControl_DragEnter;
            this.ListViewControl.DragDrop += ListViewControl_DragDrop;

            watcher.Created += watcher_Created;
            watcher.Deleted += watcher_Deleted;
            watcher.Renamed += watcher_Renamed;
        }

        protected override List<ColumnListViewItem> loadData(CancellationToken token) {
            try {
                List<ColumnListViewItem> result = new List<ColumnListViewItem>();
                List<string> patterns = Host.GetPreferenceList("directorycolumn.directory_exclude_patterns");

                foreach (DirectoryInfo dI in this.DirectoryData.GetDirectories()) {
                    token.ThrowIfCancellationRequested();
                    if (patterns.Any(x => StringHelper.MatchesPattern(dI.FullName, x))) continue;

                    result.Add(new ColumnListViewItem() {
                        SubColumn = new ColumnData(this.GetType().FullName, dI.FullName),
                        Text = dI.Name,
                        Tag = dI,
                        ImageKey = ".folder"
                    });
                }

                patterns = Host.GetPreferenceList("directorycolumn.file_exclude_patterns");

                foreach (FileInfo fI in this.DirectoryData.GetFiles()) {
                    token.ThrowIfCancellationRequested();
                    if (patterns.Any(x => StringHelper.MatchesPattern(fI.FullName, x))) continue;

                    ColumnListViewItem item = new ColumnListViewItem() {
                        Text = fI.Name,
                        Tag = fI,
                    };
                    item.ImageKey = getImageKey(item);
                    result.Add(item);
                }

                watcher.Path = DirectoryData.FullName;
                watcher.EnableRaisingEvents = true;

                return result;
            } catch (DirectoryNotFoundException) {
                throw new ShowErrorException("Directory not found.");
            } catch (UnauthorizedAccessException) {
                throw new ShowErrorException("You have no permissions to access this directory.");
            } catch (PathTooLongException) {
                throw new ShowErrorException("Path is too long.");
            }
        }

        public override string GetHeaderText() {
            if (DirectoryData.Root.FullName == DirectoryData.FullName) {
                try {
                    DriveInfo drive = new DriveInfo(DirectoryData.FullName);
                    if (!drive.IsReady || drive.VolumeLabel.Trim() == "") return DirectoryData.Name;
                    return drive.VolumeLabel + " (" + DirectoryData.Name.Replace(Path.DirectorySeparatorChar, ')');
                } catch (ArgumentException) { }
            }

            return DirectoryData.Name;
        }

        public override Image GetIcon() {
            return FugueIcons.FolderOpen;
        }

        public override Image GetIcon(ColumnListViewItem item) {
            if (item.Tag is DirectoryInfo) return FugueIcons.FolderOpen;

            List<string> patterns = Host.GetPreferenceList("directorycolumn.individual_icon_files");
            if (!patterns.Any(x => StringHelper.MatchesPattern(item.Text, x))) return base.GetIcon(item);

            FileInfo fI = item.Tag as FileInfo;
            return Etier.IconHelper.IconReader.GetFileIcon(fI.FullName, Etier.IconHelper.IconReader.IconSize.Small, false).ToBitmap();
        }

        public override List<ItemsColumn> GetTrail() {
            if (!DirectoryData.Exists) return base.GetTrail();

            DirectoryInfo current = this.DirectoryData;
            List<ItemsColumn> trail = new List<ItemsColumn>();
            trail.Add(this);

            while (current.Root.FullName != current.FullName) {
                current = current.Parent;
                trail.Add(new DirectoryColumn(current, Host));
            }

            trail.Reverse();
            return trail;
        }

        public override ItemsColumn Duplicate() {
            return new DirectoryColumn(this.DirectoryData, this.Host);
        }

        public string GetUniqueItemName(string draft) {
            if (!draft.Contains("{c}")) draft += "{c}";

            string newName = Path.Combine(ItemsPath, draft.Replace("{c}", ""));

            if (!File.Exists(newName) && !Directory.Exists(newName))
                return draft.Replace("{c}", "");

            int count = 2;
            do {
                newName = Path.Combine(ItemsPath, draft.Replace("{c}", " " + count++));
            } while (File.Exists(newName) || Directory.Exists(newName));

            return Path.GetFileName(newName);
        }

        #region Drag & Drop

        private void ListViewControl_ItemDrag(object sender, ItemDragEventArgs e) {
            string[] items = new string[ListViewControl.SelectedItems.Count];
            int i = 0;

            foreach (ColumnListViewItem item in ListViewControl.SelectedItems) {
                string path;
                if (item.Tag is DirectoryInfo) path = (item.Tag as DirectoryInfo).FullName;
                else path = (item.Tag as FileInfo).FullName;

                items[i++] = path;
            }

            ListViewControl.DoDragDrop(new DataObject(DataFormats.FileDrop, items), DragDropEffects.Copy);
        }

        private void ListViewControl_DragEnter(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] items = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (items.All(x => {
                if (File.Exists(x)) {
                    return new FileInfo(x).Directory.FullName == DirectoryData.FullName;
                } else if (Directory.Exists(x)) {
                    return new DirectoryInfo(x).Parent.FullName == DirectoryData.FullName;
                }

                return true;
            })) return;

            e.Effect = DragDropEffects.Copy;
        }

        private void ListViewControl_DragDrop(object sender, DragEventArgs e) {
            if (e.Effect != DragDropEffects.Copy) return;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] items = e.Data.GetData(DataFormats.FileDrop) as string[];
            Host.EnqueueAction(new FilesCopyAction(items, this.DirectoryData, Host));
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
            if (isDir) item.SubColumn = new ColumnData(this.GetType().FullName, (item.Tag as DirectoryInfo).FullName);

            ListViewControl.Items.Add(item);
            ListViewControl.Sort();

            OnLoadingCompleted();
        }

        private void watcher_Renamed(object sender, RenamedEventArgs e) {
            watcher_Deleted(sender, new FileSystemEventArgs(WatcherChangeTypes.Deleted, DirectoryData.FullName, e.OldName));
            watcher_Created(sender, new FileSystemEventArgs(WatcherChangeTypes.Created, DirectoryData.FullName, e.Name));
        }

        #endregion

        #region Context menu

        private void initializeContextMenu() {
            contextMenu = new ContextMenuStrip();
            this.ListViewControl.ContextMenuStrip = contextMenu;

            ToolStripItem getInfo = new ToolStripMenuItem("Get &Info") { ShortcutKeys = Keys.Control | Keys.Space };
            ToolStripItem openWith = new ToolStripMenuItem("&Open With");
            ToolStripItem rename = new ToolStripMenuItem("Re&name") { ShortcutKeys = Keys.F2 };
            ToolStripItem recycle = new ToolStripMenuItem("&Recycle") { ShortcutKeys = Keys.Delete };
            ToolStripItem selectAll = new ToolStripMenuItem("Select &All") { ShortcutKeys = Keys.Control | Keys.A };
            selectAll.Click += (_, __) => { foreach (ListViewItem item in ListViewControl.Items) item.Selected = true; };
            ToolStripItem newDirectory = new ToolStripMenuItem("New &Directory") {
                ShortcutKeys = Keys.Control | Keys.Shift | Keys.N
            };
            newDirectory.Click += newDirectory_Click;
            ToolStripItem newFile = new ToolStripMenuItem("New &File") { ShortcutKeys = Keys.Control | Keys.N };
            newFile.Click += newFile_Click;

            contextMenu.Items.AddRange(new ToolStripItem[] { 
                getInfo,
                new ToolStripSeparator(),
                openWith,
                rename,
                recycle,
                new ToolStripSeparator(),
                selectAll, 
                new ToolStripSeparator(),
                newDirectory, 
                newFile 
            });
        }

        private void newDirectory_Click(object sender, EventArgs e) {
            string directoryName = GetUniqueItemName("New Directory");

            try {
                Directory.CreateDirectory(Path.Combine(DirectoryData.FullName, directoryName));
            } catch (UnauthorizedAccessException) {
                MessageBox.Show(
                    "You have no authorization to create a new directory here.",
                    "Trail", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            } catch {
                MessageBox.Show("A new directory can't be created here.", "Trail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newFile_Click(object sender, EventArgs e) {
            string fileName = GetUniqueItemName("New Text File{c}.txt");

            try {
                File.CreateText(Path.Combine(DirectoryData.FullName, fileName)).Close();
            } catch (UnauthorizedAccessException) {
                MessageBox.Show(
                    "You have no authorization to create a new file here.",
                    "Trail", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            } catch {
                MessageBox.Show("A new file can't be created here.", "Trail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        protected override void OnItemActivate(ColumnListViewItem item) {
            if (!(item.Tag is FileInfo) || item.SubColumn != null) return;

            ProcessStartInfo info = new ProcessStartInfo((item.Tag as FileInfo).FullName);
            info.WorkingDirectory = (item.Tag as FileInfo).DirectoryName;
            Process.Start(info);

            base.OnItemActivate(item);
        }

        private string getImageKey(ColumnListViewItem item) {
            if (item.Tag is DirectoryInfo) return ".folder";

            FileInfo fI = item.Tag as FileInfo;
            string ext = Path.GetExtension(fI.Name).Replace(".", "");

            if (ext == "") return fI.FullName;

            List<string> patterns = Host.GetPreferenceList("directorycolumn.individual_icon_files");
            if (patterns.Any(x => StringHelper.MatchesPattern(fI.Name, x))) return fI.FullName;

            return ext;
        }
    }
}
