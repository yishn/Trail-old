using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Controls;

namespace Trail.Modules {
    public class NavigatingTabBar : TabBar {
        public new NavigatingTab CurrentTab { get { return base.CurrentTab as NavigatingTab; } set { base.CurrentTab = value; } }

        public NavigatingTabBar() {
            this.CurrentTabChanged += NavigatingTabBar_CurrentTabChanged;
        }

        private void NavigatingTabBar_CurrentTabChanged(object sender, EventArgs e) {
            foreach (NavigatingTab t in this.Tabs) {
                t.ColumnView.Visible = false;
            }

            if (this.CurrentTab == null) return;
            this.CurrentTab.ColumnView.Visible = true;

            if (this.CurrentTab.ColumnView.Columns.Count  != 0)
                this.CurrentTab.ColumnView.Columns[this.CurrentTab.ColumnView.Columns.Count - 1].Focus();
        }
    }
}
