using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public class ColumnListViewItem : ListViewItem {
        public ColumnControl Column { get; set; }
    }
}
