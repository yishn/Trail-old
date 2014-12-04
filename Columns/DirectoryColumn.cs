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

        public override List<ColumnListViewItem> LoadData(DoWorkEventArgs e) {
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

                result.Add(new ColumnListViewItem() {
                    Text = fI.Name,
                    Tag = fI,
                    ImageKey = ext == "" ? ".file" : ext
                });
            }

            return result;
        }

        public override void ItemActivated(ColumnListViewItem item) {
            if (!(item.Tag is FileInfo)) return;

            ProcessStartInfo info = new ProcessStartInfo((item.Tag as FileInfo).FullName);
            info.WorkingDirectory = (item.Tag as FileInfo).DirectoryName;
            Process.Start(info);
        }
    }
}
