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
using Trail.Fx;

namespace Trail.Controls {
    public partial class TabBar : UserControl {
        private Tab _currentTab = null;
        private Animation _animation;

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
        public bool AllowNoTabs { get; set; }

        public event EventHandler CurrentTabChanged;
        public event EventHandler AddButtonClicked;
        public event EventHandler<Tab> TabClosed;

        public TabBar() {
            InitializeComponent();

            this.AllowNoTabs = false;
            this.Tabs = new ObservableCollection<Tab>();
            this.AccentColor = Color.FromArgb(0, 122, 204);
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 0, 0, 0);
            btnAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0, 0);

            this.Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        public void RearrangeTabs() {
            this.SuspendLayout();
            int left = 0;

            foreach (Tab t in this.Tabs) {
                t.Top = 0;
                t.Left = left;

                left += t.Width;
            }

            pnlTabs.Width = left;
            btnAdd.Left = left;
            this.ResumeLayout();
        }

        public void RecolorTabs() {
            this.SuspendLayout();

            foreach (Tab t in this.Tabs) {
                t.BackColor = t == CurrentTab ? this.AccentColor : this.BackColor;
                t.ForeColor = t == CurrentTab ? Color.White : this.ForeColor;
                t.AutoHideClose = t != CurrentTab;
            }

            this.ResumeLayout();
        }

        public void CloseTab(Tab tab) {
            if (!AllowNoTabs && this.Tabs.Count == 1) return;
            if (_animation == null || !_animation.Enabled) _animation = new Animation();
            else return;

            int i = this.Tabs.IndexOf(tab);
            if (i == -1) return;

            if (this.Tabs.Count == 0) this.CurrentTab = null;
            else if (this.CurrentTab == tab) this.CurrentTab = this.Tabs[i == 0 ? i + 1 : i - 1];

            // Animation
            this.Tabs[i].BringToFront();
            int width = pnlTabs.Width;
            _animation.Tick += (_, value) => {
                this.Tabs[i].Top = (int)(value * this.Tabs[i].Height);

                if (i + 1 < this.Tabs.Count)
                    this.Tabs[i + 1].Left = this.Tabs[i].Right - (int)(value * this.Tabs[i].Width);

                for (int j = i + 2; j < this.Tabs.Count; j++) {
                    this.Tabs[j].Left = this.Tabs[j - 1].Right;
                }

                pnlTabs.Width = width - (int)(value * this.Tabs[i].Width);
                btnAdd.Left = pnlTabs.Right;
            };
            _animation.Complete += (_, evt) => {
                this.Tabs.RemoveAt(i);
                if (TabClosed != null) TabClosed(this, tab);
            };
            _animation.Start();
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (Tab t in e.NewItems) {
                    pnlTabs.Controls.Add(t);

                    t.MouseEnter += Tab_MouseEnter;
                    t.MouseLeave += Tab_MouseLeave;
                    t.SizeChanged += Tab_SizeChanged;
                    t.MouseClick += Tab_MouseClick;
                    t.CloseButtonClick += Tab_CloseButtonClick;
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

        private void Tab_CloseButtonClick(object sender, EventArgs e) {
            CloseTab(sender as Tab);
        }

        private void Tab_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) this.CurrentTab = sender as Tab;
            if (e.Button == MouseButtons.Middle) CloseTab(sender as Tab);
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
            t.BackColor = Color.FromArgb(200, this.AccentColor);
            t.ForeColor = Color.White;
        }

        private void pnlAccent_MouseLeave(object sender, EventArgs e) {
            RecolorTabs();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (AddButtonClicked != null) AddButtonClicked(this, e);
        }
    }
}
