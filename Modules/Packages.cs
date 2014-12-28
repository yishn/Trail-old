using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;

namespace Trail.Modules {
    public static class Packages {
        public static Dictionary<Tuple<string, string>, Action<ItemsColumn, ItemsColumn, ColumnListViewItem[]>> DragDropHandlers
            = new Dictionary<Tuple<string, string>, Action<ItemsColumn, ItemsColumn, ColumnListViewItem[]>>();
    }
}
