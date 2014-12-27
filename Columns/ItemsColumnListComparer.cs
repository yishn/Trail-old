using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.Columns {
    public class ItemsColumnListComparer : IComparer {
        public int Compare(object x, object y) {
            ColumnListViewItem item1 = (ColumnListViewItem)x;
            ColumnListViewItem item2 = (ColumnListViewItem)y;

            if (item1.ImageKey == ".folder" && item2.ImageKey != ".folder") return -1;
            if (item1.ImageKey != ".folder" && item2.ImageKey == ".folder") return 1;
            return item1.Text.CompareTo(item2.Text);
        }
    }
}
