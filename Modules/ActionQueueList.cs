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
        private IntAnimation animation = new IntAnimation();

        public ObservableCollection<ItemsActionControl> Items { get; private set; }

        public ActionQueueList() {
            InitializeComponent();
            this.Height = 0;
            this.Items = new ObservableCollection<ItemsActionControl>();

            Items.CollectionChanged += ActionProgressControls_CollectionChanged;
        }

        public bool EnqueueAction(ItemsActionControl action) {
            this.Items.Add(action);
            if (this.Items.Count == 1) action.Start();
            return true;
        }

        public void UpdateSize() {
            if (animation.Enabled) animation.Stop();

            int end = Math.Min(this.Items.Count, 3) * new ActionProgressControl().Height;
            if (end != 0) end += headerLabel.Height;

            animation = new IntAnimation();
            animation.Start(this.Height, end).Tick += (_, value) => {
                this.Height = value;
            };
        }

        private void ActionQueueList_Load(object sender, EventArgs e) {
            UpdateSize();
        }

        private void ActionProgressControls_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (ItemsActionControl c in e.NewItems) {
                    actionsList.Controls.Add(c);

                    c.Completed += Action_Completed;
                }
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                foreach (ItemsActionControl c in e.OldItems) {
                    actionsList.Controls.Remove(c);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Reset) {
                actionsList.Controls.Clear();
            }

            UpdateSize();
        }

        private void Action_Completed(object sender, EventArgs e) {
            this.Items.Remove(sender as ItemsActionControl);
            if (this.Items.Count > 0) this.Items[0].Start();
        }
    }
}
