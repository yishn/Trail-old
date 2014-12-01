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

namespace Trail.Modules {
    public class IconLoaderQueue : BackgroundWorker {
        private Queue<ColumnListViewItem> _queue = new Queue<ColumnListViewItem>();

        public ImageList ImageList { get; set; }

        public IconLoaderQueue() {
            this.WorkerReportsProgress = true;

            this.DoWork += IconLoaderQueue_DoWork;
            this.ProgressChanged += IconLoaderQueue_ProgressChanged;
        }

        public void Enqueue(ColumnListViewItem item) {
            if (ImageList.Images.Keys.Contains(item.ImageKey)) return;

            _queue.Enqueue(item);
            if (!this.IsBusy) this.RunWorkerAsync();
        }

        public void Enqueue(ItemsColumn column) {
            foreach (ColumnListViewItem item in column.ListViewControl.Items) {
                this.Enqueue(item);
            }
        }

        private void IconLoaderQueue_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            Tuple<ColumnListViewItem, Image> tuple = e.UserState as Tuple<ColumnListViewItem, Image>;
            if (ImageList != null) ImageList.Images.Add(tuple.Item1.ImageKey, tuple.Item2);

            tuple.Item1.ListView.SmallImageList = null;
            tuple.Item1.ListView.SmallImageList = this.ImageList;
        }

        private void IconLoaderQueue_DoWork(object sender, DoWorkEventArgs e) {
            while (_queue.Count > 0) {
                ColumnListViewItem item = _queue.Dequeue();
                ListView listView = item.ListView;
                ImageList images = listView.SmallImageList;
                ItemsColumn column = listView.Parent as ItemsColumn;

                Image image = column.GetIcon(item);
                Image final = new Bitmap(20, 20);
                Graphics g = Graphics.FromImage(final);

                g.DrawImage(image, new Point(2, 2));
                this.ReportProgress(0, new Tuple<ColumnListViewItem, Image>(item, final));

                g.Dispose();
                image.Dispose();
            }
        }
    }
}
