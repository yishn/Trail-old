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
        public DirectoryInfo Directory { get; set; }

        public DirectoryColumn(DirectoryInfo directory) : base() {
            this.Directory = directory;
            this.HeaderText = directory.Name;
            //this.ListViewControl.LabelEdit = true;
        }

        public override List<ColumnItem> LoadData(DoWorkEventArgs e) {
            List<ColumnItem> result = new List<ColumnItem>();

            foreach (DirectoryInfo dI in this.Directory.GetDirectories()) {
                if (e.Cancel) return null;

                result.Add(new ColumnItem() {
                    SubColumn = new DirectoryColumn(dI),
                    Text = dI.Name,
                    Tag = dI,
                    ImageKey = ".folder"
                });
            }

            foreach (FileInfo fI in this.Directory.GetFiles()) {
                if (e.Cancel) return null;

                result.Add(new ColumnItem() {
                    Text = fI.Name,
                    Tag = fI,
                    ImageKey = Path.GetExtension(fI.FullName)
                });
            }

            return result;
        }

        public override int Compare(ColumnItem item1, ColumnItem item2) {
            if (item1.Tag is DirectoryInfo && item2.Tag is FileInfo) return -1;
            if (item1.Tag is FileInfo && item2.Tag is DirectoryInfo) return 1;
            return item1.Text.CompareTo(item2.Text);
        }

        public override void ItemActivated(ColumnItem item) {
            if (!(item.Tag is FileInfo)) return;

            ProcessStartInfo info = new ProcessStartInfo((item.Tag as FileInfo).FullName);
            info.WorkingDirectory = (item.Tag as FileInfo).DirectoryName;
            Process.Start(info);
        }
    }
}
