using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;

namespace Trail.DataTypes {
    public class ColumnData {
        public string ColumnType { get; set; }
        public string Path { get; set; }

        public ColumnData() : this(typeof(EmptyColumn).FullName, "") { }

        public ColumnData(string columnType, string path) {
            this.ColumnType = columnType;
            this.Path = path;
        }

        public ItemsColumn Instantiation(IHost host) {
            Type type = Type.GetType(this.ColumnType);
            return Activator.CreateInstance(type, this.Path, host) as ItemsColumn;
        }
    }
}
