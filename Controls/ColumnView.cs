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
        public ImageList ImageList { get; set; }

        public event EventHandler<ColumnEventArgs> SubColumnAdded;

        public ColumnView() {
            InitializeComponent();

            this.Columns = new ObservableCollection<ColumnControl>();
            this.DefaultColumnWidth = 200;
            this.ScrollAnimation = new IntAnimation();

            this.Columns.CollectionChanged += Columns_CollectionChanged;
        }

        public void ScrollToLastColumn() {
            if (this.ScrollAnimation.Enabled) return;
               
            this.ScrollAnimation = new IntAnimation();
            ColumnControl column = this.Columns[this.Columns.Count - 1];
            int start = pnlColumns.HorizontalScroll.Value;
            int end = Math.Min(column.Right + pnlColumns.HorizontalScroll.Value - pnlColumns.Width, pnlColumns.HorizontalScroll.Maximum);

            if (start >= end) return;

            this.ScrollAnimation.Start(start, end).Tick += (_, e) => {
                pnlColumns.HorizontalScroll.Value = e.Value;
            };
            this.ScrollAnimation.Complete += (_, e) => {
                column.Focus();
                pnlColumns.AutoScrollMinSize = new Size(0, 0);
            };
        }

        private void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (ColumnControl c in e.NewItems) {
                    c.Width = this.DefaultColumnWidth;
                    c.Dock = DockStyle.Left;

                    c.ListViewControl.SmallImageList = this.ImageList;
                    c.ListViewControl.SelectedIndexChanged += (_, evt) => {
                        ColumnControl_SelectedIndexChanged(c, evt);
                    };

                    pnlColumns.Controls.Add(c);
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

        private void ColumnControl_SelectedIndexChanged(object sender, EventArgs e) {
            ColumnControl c = sender as ColumnControl;
            if (c.ListViewControl.SelectedIndices.Count == 0) return;
            ColumnItem item = c.ListViewControl.SelectedItems[0] as ColumnItem;
            if (item.SubColumn == null) return;

            // Remove columns on the right
            int i = this.Columns.IndexOf(c);
            int residueCount = this.Columns.Count - i - 1;

            pnlColumns.AutoScrollMinSize = new Size(pnlColumns.HorizontalScroll.Maximum + 1, 0);

            this.pnlColumns.SuspendLayout();
            for (int j = 1; j <= residueCount; j++)
                this.Columns.RemoveAt(i + 1);

            if (c.ListViewControl.SelectedItems.Count == 1) {
                // Add new column
                this.Columns.Add(item.SubColumn);
                this.pnlColumns.ResumeLayout();
                if (SubColumnAdded != null) SubColumnAdded(this, new ColumnEventArgs(item.SubColumn));
            } else {
                this.pnlColumns.ResumeLayout();
            }
        }
    }
}
