﻿using System.Collections.Generic;
using System.Threading;
using Trail.Controls;
using Trail.DataTypes;
using Trail.Templates;

namespace Trail.Columns {
    public class EmptyColumn : ItemsColumn {
        public EmptyColumn(IHost host) : base("", host) {
            this.ListViewControl.Visible = false;
        }
        public EmptyColumn(string itemsPath, IHost host) : this(host) { }

        protected override List<ColumnListViewItem> loadData(CancellationToken token) {
            return new List<ColumnListViewItem>();
        }

        public override string GetHeaderText() {
            return "New Tab";
        }

        public override ItemsColumn Duplicate() {
            return new EmptyColumn(Host);
        }
    }
}
