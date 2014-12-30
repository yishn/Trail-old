using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Trail.Helpers;

namespace Trail.Modules {
    public class FilterListComparer : IComparer {
        private static string filter;
        public static string Filter { get { return filter; } set { filter = value.ToLower(); } }

        public int Compare(object x, object y) {
            ListViewItem item1 = (ListViewItem)x;
            ListViewItem item2 = (ListViewItem)y;

            // Check filter
            bool filtered1 = item1.Text.ToLower().Contains(Filter ?? "");
            bool filtered2 = item2.Text.ToLower().Contains(Filter ?? "");
            item1.ForeColor = filtered1 ? Color.Black : Color.Gray;
            item2.ForeColor = filtered2 ? Color.Black : Color.Gray;

            if (filtered1 && !filtered2) return -1;
            if (!filtered1 && filtered2) return 1;

            // Check folder
            if (item1.ImageKey == ".folder" && item2.ImageKey != ".folder") return -1;
            if (item1.ImageKey != ".folder" && item2.ImageKey == ".folder") return 1;

            // Check text
            return item1.Text.CompareTo(item2.Text);
        }
    }
}
