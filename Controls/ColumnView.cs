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
using Trail.Fx;

namespace Trail.Controls {
    public partial class ColumnView : UserControl {
        public ObservableCollection<ColumnControl> Columns { get; private set; }
        public int DefaultColumnWidth { get; set; }
        public IntAnimation ScrollAnimation { get; private set; }

        public ColumnView() {
            InitializeComponent();

            this.Columns = new ObservableCollection<ColumnControl>();
            this.DefaultColumnWidth = 200;
            this.ScrollAnimation = new IntAnimation(20);

            this.Columns.CollectionChanged += Columns_CollectionChanged;
        }

        public void ScrollToEnd() {
            int start = pnlColumns.HorizontalScroll.Value;
            int end = pnlColumns.HorizontalScroll.Maximum - pnlColumns.ClientSize.Width + 1;

            this.ScrollAnimation.Start(start, end).Tick += (s, e) => {
                pnlColumns.HorizontalScroll.Value = e.Value;
            };
            this.ScrollAnimation.Complete += (s, e) => {
                pnlColumns.Controls[0].Focus();
            };
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
