using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;
using Trail.Controls;
using Trail.DataTypes;

namespace Trail.Modules {
    public class NavigatingTabBar : TabBar {
        public new NavigatingTab CurrentTab { get { return base.CurrentTab as NavigatingTab; } set { base.CurrentTab = value; } }

        public NavigatingTabBar() {
            this.CurrentTabChanged += NavigatingTabBar_CurrentTabChanged;
        }

        public void LoadSession() {
            this.Tabs.Clear();

            foreach (ColumnData data in Persistence.Session) {
                ItemsColumn column = data.Instantiation();
                NavigatingColumnView columnView = new NavigatingColumnView();
                columnView.NavigateTo(column.GetTrail());

                NavigatingTab tab = new NavigatingTab() {
                    Text = column.HeaderText,
                    ColumnView = columnView
                };

                this.Tabs.Add(tab);
            }

            base.CurrentTab = this.Tabs[0];
        }

        private void NavigatingTabBar_CurrentTabChanged(object sender, EventArgs e) {
            foreach (NavigatingTab t in this.Tabs) {
                t.ColumnView.Visible = false;
            }

            if (this.CurrentTab == null) return;
            this.CurrentTab.ColumnView.Visible = true;

            if (this.CurrentTab.ColumnView.LastColumn != null)
                this.CurrentTab.ColumnView.LastColumn.Focus();
        }
    }
}
