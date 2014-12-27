using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Columns {
    public class ColumnListViewItem : ListViewItem {
        public ItemsColumn SubColumn { get; set; }
    }
}
