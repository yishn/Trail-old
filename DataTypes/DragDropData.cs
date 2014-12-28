using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;

namespace Trail.DataTypes {
    public class DragDropData {
        public ItemsColumn SourceColumn { get; set; }
        public Type DestinationColumnType { get; set; }
        public ColumnListViewItem[] Items { get; set; }

        public DragDropData(ItemsColumn srcColumn, Type destColumnType, ColumnListViewItem[] items) {
            this.SourceColumn = srcColumn;
            this.DestinationColumnType = destColumnType;
            this.Items = items;
        }

        public Tuple<Type, Type> GetDragDropHandlerKey() {
            return new Tuple<Type, Type>(SourceColumn.GetType(), DestinationColumnType);
        }
    }
}
