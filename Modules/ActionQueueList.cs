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

namespace Trail.Modules {
    public partial class ActionQueueList : UserControl {
        public ObservableCollection<ActionProgressControl> Items { get; private set; }

        public ActionQueueList() {
            InitializeComponent();
            Items = new ObservableCollection<ActionProgressControl>();

            Items.CollectionChanged += ActionProgressControls_CollectionChanged;
        }

        private void ActionProgressControls_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (ActionProgressControl c in e.NewItems) {
                    actionsList.Controls.Add(c);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                foreach (ActionProgressControl c in e.OldItems) {
                    actionsList.Controls.Remove(c);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Reset) {
                actionsList.Controls.Clear();
            }
        }
    }
}
