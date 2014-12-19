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

        public event EventHandler Navigated;

        private void ColumnView_Navigated(object sender, EventArgs e) {
            this.Text = ColumnView.LastColumn.HeaderText;
            if (Navigated != null) Navigated(this, EventArgs.Empty);
        }

        public NavigatingTab() {
            this.ColumnView = new NavigatingColumnView();
        }
        public NavigatingTab(ItemsColumn column) : this() {
            this.ColumnView.NavigateTo(column);
            this.Text = column.HeaderText;
        }
    }
}
