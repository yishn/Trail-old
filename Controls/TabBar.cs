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
        private Tab _currentTab = null;

        public Color AccentColor { get { return pnlAccent.BackColor; } set { pnlAccent.BackColor = value; } }
        public ObservableCollection<Tab> Tabs { get; private set; }
        public Tab CurrentTab {
            get { return _currentTab; }
            set {
                _currentTab = value;
                RecolorTabs();
                if (CurrentTabChanged != null) CurrentTabChanged(this, new EventArgs());
            }
        }
        public bool ShowNewTabButton { get { return btnAdd.Visible; } set { btnAdd.Visible = value; } }

        public event EventHandler CurrentTabChanged;

        public TabBar() {
            InitializeComponent();
            this.Tabs = new ObservableCollection<Tab>();
            this.AccentColor = Color.FromArgb(0, 122, 204);
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 0, 0, 0);
            btnAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0, 0);

            this.Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        public void RearrangeTabs() {
            int left = 0;

            foreach (Tab t in this.Tabs) {
                t.Top = 0;
                t.Left = left;

                left += t.Width;
            }

            pnlTabs.Width = left;
            btnAdd.Left = left;
        }

        public void RecolorTabs() {
            foreach (Tab t in this.Tabs) {
                t.BackColor = t == CurrentTab ? this.AccentColor : this.BackColor;
                t.ForeColor = t == CurrentTab ? Color.White : this.ForeColor;
                t.AutoHideClose = t != CurrentTab;
            }
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (Tab t in e.NewItems) {
                    pnlTabs.Controls.Add(t);
                    t.MouseEnter += Tab_MouseEnter;
                    t.MouseLeave += Tab_MouseLeave;
                    t.SizeChanged += Tab_SizeChanged;
                }
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                foreach (Tab t in e.OldItems) {
                    pnlTabs.Controls.Remove(t);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Reset) {
                pnlTabs.Controls.Clear();
            }

            RearrangeTabs();
        }

        private void Tab_SizeChanged(object sender, EventArgs e) {
            RearrangeTabs();
        }

        private void Tab_MouseLeave(object sender, EventArgs e) {
            Tab t = sender as Tab;
            if (this.CurrentTab == t) return;
            t.BackColor = this.BackColor;
            t.ForeColor = this.ForeColor;
        }

        private void Tab_MouseEnter(object sender, EventArgs e) {
            Tab t = sender as Tab;
            if (this.CurrentTab == t) return;
            t.BackColor = Color.FromArgb(180, this.AccentColor);
            t.ForeColor = Color.White;
        }

        private void pnlAccent_MouseLeave(object sender, EventArgs e) {
            RecolorTabs();
        }
    }
}
