﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Trail.Fx;

namespace Trail.Controls {
    public partial class ColumnView : UserControl {
        private ImageList imageList;

        public ObservableCollection<ColumnControl> Columns { get; private set; }
        public ColumnControl LastColumn { get { return this.Columns.Count == 0 ? null : this.Columns[this.Columns.Count - 1]; } }
        public int DefaultColumnWidth { get; set; }
        public IntAnimation ScrollAnimation { get; private set; }
        public ImageList ImageList {
            get { return imageList; }
            set { 
                imageList = value;
                foreach (ColumnControl c in this.Columns) {
                    c.ListViewControl.SmallImageList = value;
                }
            }
        }

        public ColumnView() {
            InitializeComponent();

            this.Columns = new ObservableCollection<ColumnControl>();
            this.ScrollAnimation = new IntAnimation();

            this.Columns.CollectionChanged += Columns_CollectionChanged;
        }

        public void ScrollToLastColumn() {
            if (this.ScrollAnimation.Enabled) return;
            if (this.Columns.Count == 0) return;
               
            this.ScrollAnimation = new IntAnimation();

            int start = ScrollPanel.HorizontalScroll.Value;
            int end = this.LastColumn.Right + ScrollPanel.HorizontalScroll.Value - ScrollPanel.Width;
            end = Math.Max(Math.Min(end, ScrollPanel.HorizontalScroll.Maximum), start);

            this.ScrollAnimation.Start(start, end).Tick += (_, value) => {
                ScrollPanel.HorizontalScroll.Value = value;
            };
            this.ScrollAnimation.Complete += (_, e) => {
                this.LastColumn.Focus();
                if (end != start) UpdateScrollMinSize();
            };
        }

        public void ScrollToFirstColumn() {
            if (this.ScrollAnimation.Enabled) return;
            if (this.Columns.Count == 0) return;

            this.ScrollAnimation = new IntAnimation();
            int start = ScrollPanel.HorizontalScroll.Value;
            int end = 0;

            this.ScrollAnimation.Start(start, end).Tick += (_, value) => {
                ScrollPanel.HorizontalScroll.Value = value;
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

                    c.ListViewControl.KeyUp += (_, evt) => { ColumnControl_KeyUp(c, evt); };

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

        private void ColumnControl_KeyUp(ColumnControl sender, KeyEventArgs e) {
            int i = this.Columns.IndexOf(sender);
            if (i == -1) return;

            if (e.KeyCode == Keys.Left) {
                this.Columns[Math.Max(i - 1, 0)].Focus();
            } else if (e.KeyCode == Keys.Right) {
                this.Columns[Math.Min(i + 1, this.Columns.Count - 1)].Focus();
            }
        }
    }
}