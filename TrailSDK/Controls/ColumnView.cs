using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Trail.Fx;

namespace Trail.Controls {
    public partial class ColumnView : UserControl {
        private ImageList imageList;

        public ObservableCollection<ColumnControl> Columns { get; private set; }
        public ColumnControl LastColumn { get { return Columns.Count == 0 ? null : Columns[Columns.Count - 1]; } }
        public ColumnControl LastFocusedColumn { get; set; }
        public int DefaultColumnWidth { get; set; }
        public IntAnimation ScrollAnimation { get; private set; }
        public ImageList ImageList {
            get { return imageList; }
            set { 
                imageList = value;
                foreach (ColumnControl c in Columns) {
                    c.ListViewControl.SmallImageList = value;
                }
            }
        }

        public ColumnView() {
            InitializeComponent();

            Columns = new ObservableCollection<ColumnControl>();
            ScrollAnimation = new IntAnimation();

            Columns.CollectionChanged += Columns_CollectionChanged;
        }

        public void ScrollToLastColumn() {
            if (ScrollAnimation.Enabled) return;
            if (Columns.Count == 0) return;

            ScrollAnimation = new IntAnimation();

            int start = ScrollPanel.HorizontalScroll.Value;
            int end = LastColumn.Right + ScrollPanel.HorizontalScroll.Value - ScrollPanel.Width;
            end = Math.Max(Math.Min(end, ScrollPanel.HorizontalScroll.Maximum), start);

            ScrollAnimation.Start(start, end).Tick += (_, value) => {
                ScrollPanel.HorizontalScroll.Value = value;
            };
            ScrollAnimation.Complete += (_, e) => {
                if (end != start) UpdateScrollMinSize();
            };
        }

        public void ScrollToFirstColumn() {
            if (ScrollAnimation.Enabled) return;
            if (Columns.Count == 0) return;

            ScrollAnimation = new IntAnimation();
            int start = ScrollPanel.HorizontalScroll.Value;
            int end = 0;

            ScrollAnimation.Start(start, end).Tick += (_, value) => {
                ScrollPanel.HorizontalScroll.Value = value;
            };
            ScrollAnimation.Complete += (_, e) => {
                Columns[0].Focus();
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
                    c.Width = DefaultColumnWidth;
                    c.Dock = DockStyle.Left;
                    c.ListViewControl.SmallImageList = ImageList;

                    c.ListViewControl.KeyUp += (_, evt) => { ColumnControl_KeyUp(c, evt); };
                    c.ListViewControl.GotFocus += (_, evt) => { ColumnControl_GotFocus(c, evt); };

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

        private void ColumnControl_GotFocus(object sender, EventArgs e) {
            LastFocusedColumn = sender as ColumnControl;
        }

        private void ColumnControl_KeyUp(ColumnControl sender, KeyEventArgs e) {
            int i = Columns.IndexOf(sender);
            if (i == -1) return;

            if (e.KeyCode == Keys.Left) {
                Columns[Math.Max(i - 1, 0)].Focus();
            } else if (e.KeyCode == Keys.Right) {
                Columns[Math.Min(i + 1, Columns.Count - 1)].Focus();
            }
        }
    }
}
