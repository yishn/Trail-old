using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public class FolderColumn : ItemsColumn {
        public DirectoryInfo Directory { get; set; }

        public FolderColumn(DirectoryInfo directory) : base() {
            this.Directory = directory;
            this.HeaderText = directory.Name;
        }

        public override List<ListViewItem> LoadData() {
            List<ListViewItem> result = new List<ListViewItem>();

            foreach (DirectoryInfo dI in this.Directory.GetDirectories()) {
                result.Add(new ListViewItem() { 
                    Text = dI.Name,
                    Tag = dI,
                    ImageKey = ".folder"
                });
            }

            foreach (FileInfo fI in this.Directory.GetFiles()) {
                result.Add(new ListViewItem() {
                    Text = fI.Name,
                    Tag = fI,
                    ImageKey = Path.GetExtension(fI.FullName)
                });
            }

            return result;
        }

        public override int Compare(ListViewItem item1, ListViewItem item2) {
            if (item1.Tag is DirectoryInfo && item2.Tag is FileInfo) return -1;
            if (item1.Tag is FileInfo && item2.Tag is DirectoryInfo) return 1;
            return item1.Text.CompareTo(item2.Text);
        }
    }
}
