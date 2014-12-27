using System.Collections.Generic;
using System.Threading;
using Trail.DataTypes;

namespace Trail.Columns {
    public class EmptyColumn : ItemsColumn {
        public EmptyColumn(IPersistence persistence) : base("", persistence) {
            this.ListViewControl.Visible = false;
        }
        public EmptyColumn(string itemsPath, IPersistence persistence) : this(persistence) { }

        protected override List<ColumnListViewItem> loadData(CancellationToken token) {
            return new List<ColumnListViewItem>();
        }

        public override ItemsColumn Duplicate() {
            return new EmptyColumn(this.Persistence);
        }

        public override string GetHeaderText() {
            return "New Tab";
        }
    }
}
