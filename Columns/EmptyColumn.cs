using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trail.Modules;

namespace Trail.Columns {
    public class EmptyColumn : ItemsColumn {
        public EmptyColumn() : base("") {
            this.ListViewControl.Visible = false;
        }
        public EmptyColumn(string itemsPath) : this() { }

        protected override List<ColumnListViewItem> loadData(CancellationToken token) {
            return new List<ColumnListViewItem>();
        }

        public override ItemsColumn Duplicate() {
            return new EmptyColumn();
        }

        public override string GetHeaderText() {
            return "New Tab";
        }
    }
}
