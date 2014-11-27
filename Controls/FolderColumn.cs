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
            // TODO
            return 0;
        }
    }
}
