﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;
using Trail.DataTypes;

namespace Trail.Columns {
    public abstract class ItemsColumn : ColumnControl {
        private CancellationTokenSource cancellation;

        public IHost Host { get; private set; }
        public string ItemsPath { get; private set; }
        public bool IsBusy { get; private set; }

        public event EventHandler LoadingCompleted;
        public event EventHandler<ColumnListViewItem> ItemActivate;

        public ItemsColumn(string itemsPath, IHost host) {
            this.ItemsPath = itemsPath;
            this.HeaderText = "";
            this.Host = host;
            this.ListViewControl.AllowDrop = true;

            this.ListViewControl.ItemActivate += ListViewControl_ItemActivate;
            this.ListViewControl.ListViewItemSorter = new ItemsColumnListComparer();
            this.ListViewControl.DragEnter += ListViewControl_DragEnter;
            this.ListViewControl.DragDrop += ListViewControl_DragDrop;
        }

        protected abstract List<ColumnListViewItem> loadData(CancellationToken token);
        public abstract string GetHeaderText();

        public virtual List<ItemsColumn> GetTrail() {
            return new List<ItemsColumn>(new ItemsColumn[] { this });
        }

        public virtual Image GetIcon(ColumnListViewItem item) {
            Icon i = Etier.IconHelper.IconReader.GetFileIcon(item.Text, Etier.IconHelper.IconReader.IconSize.Small, false);
            return i.ToBitmap();
        }

        public async void LoadItems() {
            if (this.IsBusy) return;

            this.ShowError = false;
            this.HeaderText = "";
            ListViewControl.Items.Clear();

            try {
                cancellation = new CancellationTokenSource();

                List<ColumnListViewItem> result = await Task<List<ColumnListViewItem>>.Run(() => {
                    return loadData(cancellation.Token);
                });

                this.HeaderText = GetHeaderText();
                ListViewControl.Items.AddRange(result.ToArray());
                ListViewControl.Sort();
                UpdateColumnWidth();
            } catch (OperationCanceledException) {
                // Do nothing
            } catch (ShowErrorException ex) {
                this.ShowError = true;
                this.ErrorText = ex.Message;
            }

            this.IsBusy = false;
            OnLoadingCompleted();
        }

        public void CancelLoading() {
            if (cancellation != null) cancellation.Cancel();
        }

        public ColumnData GetColumnData() {
            return new ColumnData(this.GetType().FullName, this.ItemsPath);
        }

        public ItemsColumn Duplicate() {
            return GetColumnData().Instantiation(Host);
        }

        public void Filter(string filter) {
            this.ListViewControl.BeginUpdate();

            foreach(ListViewItem item in ListViewControl.Items) {
                item.ForeColor = item.Text.Contains(filter) ? Color.Black : Color.Gray;
            }
            (ListViewControl.ListViewItemSorter as ItemsColumnListComparer).Filter = filter;
            ListViewControl.Sort();

            this.ListViewControl.EndUpdate();
        }

        #region Drag & Drop

        private void ListViewControl_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.None;
            if (!e.Data.GetDataPresent(typeof(DragDropData))) return;

            DragDropData data = e.Data.GetData(typeof(DragDropData)) as DragDropData;
            if (data.DestinationColumnType != this.GetType()) return;
            if (!Host.DragDropHandlers.ContainsKey(data.GetDragDropHandlerKey())) return;

            e.Effect = DragDropEffects.Copy;
        }

        private void ListViewControl_DragDrop(object sender, DragEventArgs e) {
            if (e.Effect != DragDropEffects.Copy) return;
            if (!e.Data.GetDataPresent(typeof(DragDropData))) return;

            DragDropData data = e.Data.GetData(typeof(DragDropData)) as DragDropData;
            Host.DragDropHandlers[data.GetDragDropHandlerKey()].Invoke(data.SourceColumn, this, data.Items);
        }

        #endregion

        #region Events

        private void ListViewControl_ItemActivate(object sender, EventArgs e) {
            OnItemActivate(ListViewControl.SelectedItems[0] as ColumnListViewItem);
        }

        protected virtual void OnLoadingCompleted() {
            if (LoadingCompleted != null) LoadingCompleted(this, EventArgs.Empty);
        }

        protected virtual void OnItemActivate(ColumnListViewItem item) {
            if (ItemActivate != null) ItemActivate(this, item);
        }

        #endregion
    }
}
