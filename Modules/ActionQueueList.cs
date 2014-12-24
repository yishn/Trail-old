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
using Trail.Controls;
using System.Collections.Specialized;
using Trail.Fx;
using Trail.Actions;

namespace Trail.Modules {
    public partial class ActionQueueList : UserControl {
        private Animation animation = new Animation();

        public ObservableCollection<ActionControl> Items { get; private set; }

        public ActionQueueList() {
            InitializeComponent();
            this.Height = 0;
            this.Items = new ObservableCollection<ActionControl>();

            Items.CollectionChanged += ActionProgressControls_CollectionChanged;
        }

        public bool AddItem(ActionControl item) {
            if (animation.Enabled) return false;
            animation = new Animation();

            int itemHeight = item.Height;
            int height = this.Height;
            item.Height = 0;
            this.Items.Add(item);

            animation.Start().Tick += (_, value) => {
                item.Height = (int)(value * itemHeight);
                this.Height = height + (int)(value * (itemHeight + (1 - Math.Sign(height)) * headerLabel.Height));
            };

            return true;
        }

        public bool RemoveItem(ActionControl item) {
            if (animation.Enabled || !this.Items.Contains(item)) return false;
            animation = new Animation();

            int itemHeight = item.Height;
            int height = this.Height;

            animation.Start().Tick += (_, value) => {
                item.Height = itemHeight - (int)(value * itemHeight);
                this.Height = height - (int)(value * (itemHeight + (1 - Math.Sign(Items.Count - 1)) * headerLabel.Height));
            };
            animation.Complete += (_, e) => {
                this.Items.Remove(item);
            };

            return true;
        }

        public void UpdateSize() {
            int end = Math.Min(this.Items.Count, 3) * new ActionControl().Height;
            if (end != 0) end += headerLabel.Height;
            this.Height = end;
        }

        private void ActionQueueList_Load(object sender, EventArgs e) {
            UpdateSize();
        }

        private void ActionProgressControls_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (ActionControl c in e.NewItems) {
                    actionsList.Controls.Add(c);

                    c.CancelButtonClicked += Item_CancelButtonClicked;
                }
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                foreach (ActionControl c in e.OldItems) {
                    actionsList.Controls.Remove(c);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Reset) {
                actionsList.Controls.Clear();
            }
        }

        private void Item_CancelButtonClicked(object sender, EventArgs e) {
            this.RemoveItem(sender as ActionControl);
        }
    }
}
