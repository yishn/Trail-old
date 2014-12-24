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
using Trail.Actions;
using System.Collections.Specialized;
using Trail.Fx;

namespace Trail.Modules {
    public partial class ActionQueueList : UserControl {
        private Animation animation = new Animation();

        public ObservableCollection<ItemsAction> Items { get; private set; }

        public ActionQueueList() {
            InitializeComponent();
            this.Height = 0;
            this.Items = new ObservableCollection<ItemsAction>();

            Items.CollectionChanged += ActionProgressControls_CollectionChanged;
        }

        public bool EnqueueAction(ItemsAction action) {
            if (animation.Enabled) return false;
            animation = new Animation();

            int itemHeight = action.Height;
            int height = this.Height;
            action.Height = 0;
            this.Items.Add(action);

            animation.Start().Tick += (_, value) => {
                action.Height = (int)(value * itemHeight);
                this.Height = height + (int)(value * (itemHeight + (1 - Math.Sign(height)) * headerLabel.Height));
            };
            animation.Complete += (_, e) => {
                action.Start();
            };

            return true;
        }

        public bool RemoveAction(ItemsAction action) {
            if (animation.Enabled || !this.Items.Contains(action)) return false;
            animation = new Animation();

            int itemHeight = action.Height;
            int height = this.Height;

            animation.Start().Tick += (_, value) => {
                action.Height = itemHeight - (int)(value * itemHeight);
                this.Height = height - (int)(value * (itemHeight + (1 - Math.Sign(Items.Count - 1)) * headerLabel.Height));
            };
            animation.Complete += (_, e) => {
                this.Items.Remove(action);
            };

            return true;
        }

        public void UpdateSize() {
            int end = Math.Min(this.Items.Count, 3) * new ActionProgressControl().Height;
            if (end != 0) end += headerLabel.Height;
            this.Height = end;
        }

        private void ActionQueueList_Load(object sender, EventArgs e) {
            UpdateSize();
        }

        private void ActionProgressControls_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (ItemsAction c in e.NewItems) {
                    actionsList.Controls.Add(c);

                    c.Completed += Action_Completed;
                }
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                foreach (ItemsAction c in e.OldItems) {
                    actionsList.Controls.Remove(c);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Reset) {
                actionsList.Controls.Clear();
            }
        }

        private void Action_Completed(object sender, RunWorkerCompletedEventArgs e) {
            this.RemoveAction(sender as ItemsAction);
        }
    }
}
