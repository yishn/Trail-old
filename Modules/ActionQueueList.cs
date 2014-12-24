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
        public ObservableCollection<ActionProgressControl> ActionProgressControls { get; private set; }

        public ActionQueueList() {
            InitializeComponent();

            ActionProgressControls = new ObservableCollection<ActionProgressControl>();

            ActionProgressControls.CollectionChanged += ActionProgressControls_CollectionChanged;
        }

        private void ActionProgressControls_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (ActionProgressControl c in e.NewItems) actionsList.Controls.Add(c);
            }
        }
    }
}
