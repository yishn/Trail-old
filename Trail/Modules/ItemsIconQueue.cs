using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Columns;
using Trail.Controls;
using Trail.Templates;

namespace Trail.Modules {
    public class ItemsIconQueue : BackgroundWorker {
        private Queue<ColumnListViewItem> queue = new Queue<ColumnListViewItem>();

        public ImageList ImageList { get; set; }

        public ItemsIconQueue() {
            this.WorkerReportsProgress = true;
        }

        public void Enqueue(ColumnListViewItem item) {
            if (ImageList == null) return;
            if (ImageList.Images.Keys.Contains(item.ImageKey)) return;

            queue.Enqueue(item);
            if (!this.IsBusy) this.RunWorkerAsync();
        }

        public void Enqueue(ItemsColumn column) {
            foreach (ColumnListViewItem item in column.ListViewControl.Items) {
                this.Enqueue(item);
            }
        }

        protected override void OnProgressChanged(ProgressChangedEventArgs e) {
            Tuple<ColumnListViewItem, Image> tuple = e.UserState as Tuple<ColumnListViewItem, Image>;
            string imageKey = tuple.Item1.ImageKey;

            if (ImageList != null && !ImageList.Images.ContainsKey(imageKey)) ImageList.Images.Add(imageKey, tuple.Item2);
            tuple.Item1.ImageKey = imageKey;

            base.OnProgressChanged(e);
        }

        protected override void OnDoWork(DoWorkEventArgs e) {
            while (queue.Count > 0) {
                ColumnListViewItem item = queue.Dequeue();
                ListView listView = item.ListView;
                ImageList images = listView.SmallImageList;
                ItemsColumn column = listView.Parent as ItemsColumn;

                Image image = column.GetIcon(item);
                Image final = new Bitmap(20, 20);
                Graphics g = Graphics.FromImage(final);

                g.DrawImage(image, new Rectangle(2, 2, 16, 16));
                this.ReportProgress(0, new Tuple<ColumnListViewItem, Image>(item, final));

                g.Dispose();
                image.Dispose();
            }
            base.OnDoWork(e);
        }
    }
}
