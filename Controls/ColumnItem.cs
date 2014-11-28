using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public class ColumnItem : ListViewItem {
        public ItemsColumn SubColumn { get; set; }
    }
}
