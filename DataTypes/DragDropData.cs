using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;

namespace Trail.DataTypes {
    public class DragDropData {
        public string SourceColumnType { get; set; }
        public string DestinationColumnType { get; set; }
        public ColumnListViewItem[] Items { get; set; }

        public DragDropData(string srcColumnType, string destColumnType, ColumnListViewItem[] items) {
            this.SourceColumnType = srcColumnType;
            this.DestinationColumnType = destColumnType;
            this.Items = items;
        }
    }
}
