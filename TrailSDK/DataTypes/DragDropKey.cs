using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Templates;

namespace Trail.DataTypes {
    public struct DragDropKey {
        public string SourceColumnType;
        public string DestinationColumnType;

        public DragDropKey(string sourceColumnType, string destinationColumnType) {
            SourceColumnType = sourceColumnType;
            DestinationColumnType = destinationColumnType;
        }
    }
}
