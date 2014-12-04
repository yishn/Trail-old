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

        public ColumnView() {
            InitializeComponent();

            this.Columns = new ObservableCollection<ColumnControl>();
            this.DefaultColumnWidth = 200;
            this.ScrollAnimation = new IntAnimation();

            this.Columns.CollectionChanged += Columns_CollectionChanged;
        }

        public void ScrollToLastColumn() {
            if (this.ScrollAnimation.Enabled) return;
            if (this.Columns.Count == 0) return;
               
            this.ScrollAnimation = new IntAnimation();
            ColumnControl column = this.Columns[this.Columns.Count - 1];

            int start = ScrollPanel.HorizontalScroll.Value;
            int end = column.Right + ScrollPanel.HorizontalScroll.Value - ScrollPanel.Width;
            end = Math.Max(Math.Min(end, ScrollPanel.HorizontalScroll.Maximum), start);

            if (end == start) return;

            this.ScrollAnimation.Start(start, end).Tick += (_, e) => {
                ScrollPanel.HorizontalScroll.Value = e.Value;
            };
            this.ScrollAnimation.Complete += (_, e) => {
                column.Focus();
                UpdateScrollMinSize();
            };
        }

        public void ScrollToFirstColumn() {
            if (this.ScrollAnimation.Enabled) return;
            if (this.Columns.Count == 0) return;

            this.ScrollAnimation = new IntAnimation();
            int start = ScrollPanel.HorizontalScroll.Value;
            int end = 0;

            this.ScrollAnimation.Start(start, end).Tick += (_, e) => {
                ScrollPanel.HorizontalScroll.Value = e.Value;
            };
            this.ScrollAnimation.Complete += (_, e) => {
                this.Columns[0].Focus();
                UpdateScrollMinSize();
            };
        }

        public void UpdateScrollMinSize() {
            ScrollPanel.AutoScrollMinSize = new Size(0, 0);
            EnlargeScrollMinSize();
        }

        public void EnlargeScrollMinSize() {
            ScrollPanel.AutoScrollMinSize = new Size(ScrollPanel.HorizontalScroll.Maximum + 1, 0);
        }

        private void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (ColumnControl c in e.NewItems) {
                    c.Width = this.DefaultColumnWidth;
                    c.Dock = DockStyle.Left;
                    c.ListViewControl.SmallImageList = this.ImageList;

                    ScrollPanel.Controls.Add(c);
                    c.BringToFront();
                }
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                foreach (ColumnControl c in e.OldItems) {
                    ScrollPanel.Controls.Remove(c);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Reset) {
                ScrollPanel.Controls.Clear();
            }
        }
    }
}
