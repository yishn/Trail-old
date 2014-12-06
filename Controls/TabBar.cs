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
using System.Collections.Specialized;

namespace Trail.Controls {
    public partial class TabBar : UserControl {
        public Color AccentColor { get { return pnlAccent.BackColor; } set { pnlAccent.BackColor = value; } }
        public ObservableCollection<Tab> Tabs { get; private set; }
        public Tab CurrentTab { get; set; }

        public TabBar() {
            InitializeComponent();
            this.Tabs = new ObservableCollection<Tab>();
            this.AccentColor = Color.FromArgb(0, 122, 204);

            this.Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        public void RearrangeTabs() {
            int left = 0;

            foreach (Tab t in this.Tabs) {
                t.Top = 2;
                t.Left = left;
                t.BackColor = t == CurrentTab ? this.AccentColor : this.BackColor;
                t.ForeColor = t == CurrentTab ? Color.White : this.ForeColor;

                left += t.Width;
            }
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (Tab t in e.NewItems) {
                    pnlTabs.Controls.Add(t);
                }

                RearrangeTabs();
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                foreach (Tab t in e.OldItems) {
                    pnlTabs.Controls.Remove(t);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Reset) {
                pnlTabs.Controls.Clear();
            }
        }

        private void TabBar_Resize(object sender, EventArgs e) {
            RearrangeTabs();
        }
    }
}
