﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Trail.Fx;

namespace Trail.Controls {
    public partial class TabBar : UserControl {
        private Tab currentTab = null;
        private Animation animation;

        public Color AccentColor { get { return pnlAccent.BackColor; } set { pnlAccent.BackColor = value; } }
        public ObservableCollection<Tab> Tabs { get; private set; }
        public Tab CurrentTab {
            get { return currentTab; }
            set {
                if (currentTab == value) return;
                currentTab = value;
                RecolorTabs();
                if (CurrentTabChanged != null) CurrentTabChanged(this, new EventArgs());
            }
        }
        public bool ShowNewTabButton { get { return addButton.Visible; } set { addButton.Visible = value; } }
        public bool AllowNoTabs { get; set; }

        public event EventHandler CurrentTabChanged;
        public event EventHandler AddButtonClicked;
        public event EventHandler<Tab> TabAdded;
        public event EventHandler<Tab> TabClosed;
        public event EventHandler<Tab> TabMoved;

        public TabBar() {
            InitializeComponent();

            var doubleBufferPropertyInfo = this.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(this, true, null);

            this.AllowNoTabs = false;
            this.Tabs = new ObservableCollection<Tab>();
            this.AccentColor = Color.FromArgb(0, 122, 204);
            addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 0, 0, 0);
            addButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0, 0);
            menuButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 0, 0, 0);
            menuButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0, 0);

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
            addButton.Left = left;
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

        public bool CloseTab(Tab tab) {
            if (!AllowNoTabs && this.Tabs.Count == 1) return false;
            if (animation == null || !animation.Enabled) animation = new Animation();
            else return false;

            int i = this.Tabs.IndexOf(tab);
            if (i == -1) return false;

            if (this.Tabs.Count == 0) this.CurrentTab = null;
            else if (this.CurrentTab == tab) this.CurrentTab = this.Tabs[i == 0 ? i + 1 : i - 1];

            // Animation
            this.Tabs[i].SendToBack();
            this.CurrentTab.BringToFront();
            int width = pnlTabs.Width;

            animation.Tick += (_, value) => {
                this.Tabs[i].Top = (int)(value * this.Tabs[i].Height);

                if (i + 1 < this.Tabs.Count)
                    this.Tabs[i + 1].Left = this.Tabs[i].Right - (int)(value * this.Tabs[i].Width);

                for (int j = i + 2; j < this.Tabs.Count; j++) {
                    this.Tabs[j].Left = this.Tabs[j - 1].Right;
                }

                pnlTabs.Width = width - (int)(value * this.Tabs[i].Width);
                addButton.Left = pnlTabs.Right;
            };
            animation.Complete += (_, evt) => {
                this.Tabs.RemoveAt(i);
                if (TabClosed != null) TabClosed(this, tab);
            };
            animation.Start();
            return true;
        }

        public bool AddTab(Tab tab) {
            if (animation == null || !animation.Enabled) animation = new Animation();
            else return false;

            int width = pnlTabs.Width;
            this.Tabs.Add(tab);

            animation.Tick += (_, value) => {
                pnlTabs.Width = width + (int)(value * tab.Width);
                addButton.Left = pnlTabs.Right;
                tab.Left = width;
                tab.Top = tab.Height - (int)(value * tab.Height);
            };
            animation.Complete += (_, e) => {
                if (TabAdded != null) TabAdded(this, tab);
            };
            animation.Start();
            return true;
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add) {
                foreach (Tab t in e.NewItems) {
                    t.Height = pnlTabs.Height;
                    pnlTabs.Controls.Add(t);

                    t.SizeChanged += Tab_SizeChanged;
                    t.MouseEnter += Tab_MouseEnter;
                    t.MouseLeave += Tab_MouseLeave;
                    t.MouseClick += Tab_MouseClick;
                    t.MouseDown += Tab_MouseDown;
                    t.MouseMove += Tab_MouseMove;
                    t.MouseUp += Tab_MouseUp;
                    t.DragEnter += t_DragEnter;
                    t.DragOver += t_DragOver;
                    t.CloseButtonClick += Tab_CloseButtonClick;
                }
            } else if (e.Action == NotifyCollectionChangedAction.Remove) {
                foreach (Tab t in e.OldItems) {
                    pnlTabs.Controls.Remove(t);
                }
            } else if (e.Action == NotifyCollectionChangedAction.Reset) {
                pnlTabs.Controls.Clear();
            }
        }

        #region Tab drag activation

        private DateTime dragEnterTime;

        private void t_DragEnter(object sender, DragEventArgs e) {
            dragEnterTime = DateTime.Now;
        }

        private void t_DragOver(object sender, DragEventArgs e) {
            if ((DateTime.Now - dragEnterTime).TotalMilliseconds < 500) return;
            this.CurrentTab = sender as Tab;
        }

        #endregion

        #region Tab moving

        private int _mousePosLeft;

        private void Tab_MouseDown(object sender, MouseEventArgs e) {
            Tab t = sender as Tab;
            t.BringToFront();
            _mousePosLeft = Control.MousePosition.X;
        }

        private void Tab_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;

            Tab t = sender as Tab;
            t.Left = Math.Max(0, Math.Min(t.Left + Control.MousePosition.X - _mousePosLeft, pnlTabs.Width - t.Width));
            _mousePosLeft = Control.MousePosition.X;

            ObservableCollection<Tab> newTabs = new ObservableCollection<Tab>();

            int left = 0;
            bool currentAdded = false;
            for (int i = 0; i < this.Tabs.Count; i++) {
                if (this.Tabs[i] == t) continue;

                if (!currentAdded && t.Left < left + this.Tabs[i].Width / 2) {
                    left += t.Width;
                    newTabs.Add(t);
                    currentAdded = true;
                }

                newTabs.Add(this.Tabs[i]);
                this.Tabs[i].Left = left;
                left += this.Tabs[i].Width;
            }
            if (!currentAdded) newTabs.Add(t);

            for (int i = 0; i < this.Tabs.Count; i++) {
                this.Tabs[i] = newTabs[i];
            }

            this.CurrentTab = t;
        }

        private void Tab_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;

            Tab t = sender as Tab;
            int j = this.Tabs.IndexOf(t);
            int left = 0;

            for (int i = 0; i < j; i++) {
                left += this.Tabs[i].Width;
            }

            animation = new IntAnimation();
            (animation as IntAnimation).Tick += (_, value) => {
                t.Left = value;
            };
            animation.Complete += (_, evt) => {
                RearrangeTabs();
                if (TabMoved != null) TabMoved(this, t);
            };
            (animation as IntAnimation).Start(t.Left, left);
        }

        #endregion

        #region Tab functions

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

        #endregion

        private void TabBar_Load(object sender, EventArgs e) {
            RearrangeTabs();
        }

        private void addButton_Click(object sender, EventArgs e) {
            if (AddButtonClicked != null) AddButtonClicked(this, e);
        }

        #region Menu

        private void menuButton_Click(object sender, EventArgs e) {
            // Create tab list
            menu.Items.Clear();

            foreach (Tab t in this.Tabs) {
                ToolStripMenuItem item = new ToolStripMenuItem(t.Text, null, (_, evt) => { this.CurrentTab = t; });
                item.Checked = this.CurrentTab == t;
                menu.Items.Add(item);
            }

            menu.Show(menuButton, new Point(menuButton.Width - menu.Width, menuButton.Height));
        }

        #endregion
    }
}