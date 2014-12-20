﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Trail.Controls {
    public partial class ColumnControl : UserControl {
        public string HeaderText { get { return headerColumn.Text; } set { headerColumn.Text = value; } }
        public bool Disabled { get { return !ListViewControl.Visible; } set { ListViewControl.Visible = !value; } }

        public ColumnControl() {
            InitializeComponent();
        }

        private void ListViewControl_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
            int padding = this.Width - ListViewControl.ClientSize.Width;
            this.Width = e.NewWidth + padding + 3;
            (this.Parent.Parent as ColumnView).EnlargeScrollMinSize();
        }

        private void ListViewControl_ClientSizeChanged(object sender, EventArgs e) {
            headerColumn.Width = ListViewControl.ClientSize.Width - 3;
        }

        public void UpdateColumnWidth() {
            this.SuspendLayout();
            ListViewControl.BeginUpdate();
            ListViewControl.Width++;
            ListViewControl.Width--;
            ListViewControl.EndUpdate();
            this.ResumeLayout();
        }
    }
}
