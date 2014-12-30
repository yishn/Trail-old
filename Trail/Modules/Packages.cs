using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;
using Trail.Controls;
using Trail.DataTypes;
using Trail.Templates;

namespace Trail.Modules {
    public static class Packages {
        public static Dictionary<Tuple<Type, Type>, Action<ItemsColumn, ItemsColumn, ColumnListViewItem[]>> DragDropHandlers
            = new Dictionary<Tuple<Type, Type>, Action<ItemsColumn, ItemsColumn, ColumnListViewItem[]>>();

        public static ItemsColumn InstantiateColumn(ColumnData data, IHost host) {
            Type type = Type.GetType(data.ColumnType);
            return Activator.CreateInstance(type, data.Path, host) as ItemsColumn;
        }
    }
}
