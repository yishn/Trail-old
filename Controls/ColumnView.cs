using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Trail.Controls {
    public partial class ColumnView : UserControl {
        public ObservableCollection<ColumnControl> Columns { get; private set; }
        public int DefaultColumnWidth { get; set; }

        public ColumnView() {
            InitializeComponent();

            this.Columns = new ObservableCollection<ColumnControl>();
            this.DefaultColumnWidth = 200;

            this.Columns.CollectionChanged += Columns_CollectionChanged;
        }

        public void ScrollToEnd() {

        }

        private void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (ColumnControl c in e.NewItems) {
                    pnlColumns.Controls.Add(c);
                    c.Width = this.DefaultColumnWidth;
                    c.Dock = DockStyle.Left;
                    c.BringToFront();
                }
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                foreach (ColumnControl c in e.OldItems) {
                    pnlColumns.Controls.Remove(c);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Reset) {
                pnlColumns.Controls.Clear();
            }
        }
    }
}
