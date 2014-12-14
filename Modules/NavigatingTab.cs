using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;
using Trail.Controls;

namespace Trail.Modules {
    public class NavigatingTab : Tab {
        private NavigatingColumnView columnView;

        public NavigatingColumnView ColumnView { 
            get { return columnView; } 
            set {
                columnView = value;
                value.Navigated += ColumnView_Navigated;
            } 
        }

        private void ColumnView_Navigated(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        public event EventHandler Navigated;

        public NavigatingTab() {
            this.ColumnView = new NavigatingColumnView();
        }
        public NavigatingTab(ItemsColumn column) : this() {
            this.ColumnView.NavigateTo(column.GetTrail());
            this.Text = column.HeaderText;
        }
    }
}
