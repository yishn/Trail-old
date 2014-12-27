using System.Windows.Forms;
using Trail.DataTypes;

namespace Trail.Columns {
    public class ColumnListViewItem : ListViewItem {
        public ColumnData SubColumn { get; set; }
    }
}
