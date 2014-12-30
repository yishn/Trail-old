using System.Windows.Forms;
using Trail.DataTypes;

namespace Trail.Controls {
    public class ColumnListViewItem : ListViewItem {
        public ColumnData SubColumn { get; set; }
    }
}
