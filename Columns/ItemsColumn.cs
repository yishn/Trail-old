using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;
using Trail.DataTypes;

namespace Trail.Columns {
    public abstract class ItemsColumn : ColumnControl {
        private CancellationTokenSource cancellation;

        public IPersistence Persistence { get; private set; }
        public string ItemsPath { get; private set; }
        public bool IsBusy { get; private set; }

        public event EventHandler LoadingCompleted;
        public event EventHandler<ColumnListViewItem> ItemActivate;

        public ItemsColumn(string itemsPath, IPersistence persistence) {
            this.ItemsPath = itemsPath;
            this.HeaderText = "";
            this.Persistence = persistence;

            this.ListViewControl.ItemActivate += ListViewControl_ItemActivate;
            this.ListViewControl.ListViewItemSorter = new ItemsColumnListComparer();
        }

        protected abstract List<ColumnListViewItem> loadData(CancellationToken token);
        public abstract string GetHeaderText();
        public abstract ItemsColumn Duplicate();

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

        private void ListViewControl_ItemActivate(object sender, EventArgs e) {
            OnItemActivate(ListViewControl.SelectedItems[0] as ColumnListViewItem);
        }

        protected virtual void OnLoadingCompleted() {
            if (LoadingCompleted != null) LoadingCompleted(this, EventArgs.Empty);
        }

        protected virtual void OnItemActivate(ColumnListViewItem item) {
            if (ItemActivate != null) ItemActivate(this, item);
        }
    }
}
