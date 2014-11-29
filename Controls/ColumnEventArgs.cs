using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trail.Controls {
    public class ColumnEventArgs {
        public ColumnControl Column { get; set; }

        public ColumnEventArgs(ColumnControl column) {
            this.Column = column;
        }
    }
}
