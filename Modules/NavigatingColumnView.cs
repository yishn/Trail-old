using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;
using Trail.Controls;

namespace Trail.Modules {
    public class NavigatingColumnView : ColumnView {
        public NavigatingColumnView() {
            this.SubColumnAdded += NavigatingColumnView_SubColumnAdded;
        }

        private void NavigatingColumnView_SubColumnAdded(object sender, ColumnEventArgs e) {
            ItemsColumn column = e.Column as ItemsColumn;
            column.LoadItems();
            this.ScrollToLastColumn();
        }
    }
}
