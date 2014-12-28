using System.Collections;
using Trail.Helpers;

namespace Trail.Columns {
    public class ItemsColumnListComparer : IComparer {
        public string Filter { get; set; }

        public ItemsColumnListComparer(string filter = "") {
            this.Filter = filter;
        }

        public int Compare(object x, object y) {
            ColumnListViewItem item1 = (ColumnListViewItem)x;
            ColumnListViewItem item2 = (ColumnListViewItem)y;

            if (item1.Text.Contains(Filter) && !item2.Text.Contains(Filter)) return -1;
            if (!item1.Text.Contains(Filter) && item2.Text.Contains(Filter)) return 1;
            if (item1.ImageKey == ".folder" && item2.ImageKey != ".folder") return -1;
            if (item1.ImageKey != ".folder" && item2.ImageKey == ".folder") return 1;
            return item1.Text.CompareTo(item2.Text);
        }
    }
}
