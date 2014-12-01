using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Controls;

namespace Trail.Modules {
    public class IconLoaderQueue : BackgroundWorker {
        public Queue<ColumnListViewItem> Queue { get; private set; }

        public IconLoaderQueue() {
            this.Queue = new Queue<ColumnListViewItem>();

            this.DoWork += IconLoaderQueue_DoWork;
        }

        public void IconLoaderQueue_DoWork(object sender, DoWorkEventArgs e) {
            while (Queue.Count > 0) {
                ColumnListViewItem item = Queue.Dequeue();
            }
        }
    }
}
