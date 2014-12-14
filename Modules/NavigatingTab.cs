using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Controls;

namespace Trail.Modules {
    public class NavigatingTab : Tab {
        public NavigatingColumnView ColumnView { get; set; }

        public NavigatingTab() {
            this.ColumnView = new NavigatingColumnView();
        }
        public NavigatingTab(string text) : this() { this.Text = text; }
    }
}
