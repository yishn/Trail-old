using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Controls;
using Trail.Templates;

namespace Trail.DataTypes {
    public class DragDropData {
        public ItemsColumn SourceColumn { get; set; }
        public ColumnListViewItem[] Items { get; set; }

        public DragDropData(ItemsColumn srcColumn, ColumnListViewItem[] items) {
            SourceColumn = srcColumn;
            Items = items;
        }
    }
}
