using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.DataTypes {
    public class FavoriteItem {
        public string ColumnType { get; set; }
        public string Path { get; set; }

        public FavoriteItem() { }

        public FavoriteItem(string columnType, string path) {
            this.ColumnType = columnType;
            this.Path = path;
        }
    }
}
