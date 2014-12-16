using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Modules;

namespace Trail.Columns {
    public class EmptyColumn : ItemsColumn {
        public EmptyColumn() : base("") {
            this.ListViewControl.Visible = false;
            this.HeaderText = "New Tab";
        }
        public EmptyColumn(string itemsPath) : this() { }

        protected override List<ColumnListViewItem> loadData(DoWorkEventArgs e) {
            return new List<ColumnListViewItem>();
        }

        public override void ItemActivated(ColumnListViewItem item) {
            // Do nothing
        }

        public override ItemsColumn Duplicate() {
            return new EmptyColumn();
        }
    }
}
