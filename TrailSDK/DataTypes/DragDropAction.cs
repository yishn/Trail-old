using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Controls;
using Trail.Templates;

namespace Trail.DataTypes {
    public delegate void DragDropAction(ItemsColumn source, ItemsColumn destination, ColumnListViewItem[] items);
}
