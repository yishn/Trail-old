using System;
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

            var doubleBufferPropertyInfo = GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(this, true, null);

            AllowNoTabs = false;
            Tabs = new ObservableCollection<Tab>();
            AccentColor = Color.FromArgb(0, 122, 204);
            addButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 0, 0, 0);
            addButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0, 0);
            menuButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 0, 0, 0);
            menuButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0, 0);

            Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        public void RearrangeTabs() {
            SuspendLayout();
            int left = 0;

            foreach (Tab t in Tabs) {
                t.Top = 0;
                t.Left = left;

                left += t.Width;
            }

            pnlTabs.Width = left;
            addButton.Left = left;
            ResumeLayout();
        }

        public void RecolorTabs() {
            SuspendLayout();

            foreach (Tab t in Tabs) {
                t.BackColor = t == CurrentTab ? AccentColor : BackColor;
                t.ForeColor = t == CurrentTab ? Color.White : ForeColor;
                t.AutoHideClose = t != CurrentTab;
            }

            ResumeLayout();
        }

        public bool CloseTab(Tab tab) {
            if (!AllowNoTabs && Tabs.Count == 1) return false;
            if (animation == null || !animation.Enabled) animation = new Animation();
            else return false;

            int i = Tabs.IndexOf(tab);
            if (i == -1) return false;

            if (Tabs.Count == 0)
                CurrentTab = null;
            else if (CurrentTab == tab)
                CurrentTab = Tabs[i == 0 ? i + 1 : i - 1];

            // Animation
            Tabs[i].SendToBack();
            CurrentTab.BringToFront();
            int width = pnlTabs.Width;

            animation.Tick += (_, value) => {
                Tabs[i].Top = (int)(value * Tabs[i].Height);

                if (i + 1 < Tabs.Count)
                    Tabs[i + 1].Left = Tabs[i].Right - (int)(value * Tabs[i].Width);

                for (int j = i + 2; j < Tabs.Count; j++) {
                    Tabs[j].Left = Tabs[j - 1].Right;
                }

                pnlTabs.Width = width - (int)(value * Tabs[i].Width);
                addButton.Left = pnlTabs.Right;
            };
            animation.Complete += (_, evt) => {
                Tabs.RemoveAt(i);
                if (TabClosed != null) TabClosed(this, tab);
            };
            animation.Start();
            return true;
        }

        public bool AddTab(Tab tab) {
            if (animation == null || !animation.Enabled) animation = new Animation();
            else return false;

            int width = pnlTabs.Width;
            Tabs.Add(tab);

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
            CurrentTab = sender as Tab;
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
            for (int i = 0; i < Tabs.Count; i++) {
                if (Tabs[i] == t) continue;

                if (!currentAdded && t.Left < left + Tabs[i].Width / 2) {
                    left += t.Width;
                    newTabs.Add(t);
                    currentAdded = true;
                }

                newTabs.Add(Tabs[i]);
                Tabs[i].Left = left;
                left += Tabs[i].Width;
            }
            if (!currentAdded) newTabs.Add(t);

            for (int i = 0; i < Tabs.Count; i++) {
                Tabs[i] = newTabs[i];
            }

            CurrentTab = t;
        }

        private void Tab_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;

            Tab t = sender as Tab;
            int j = Tabs.IndexOf(t);
            int left = 0;

            for (int i = 0; i < j; i++) {
                left += Tabs[i].Width;
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
            if (e.Button == MouseButtons.Left)
                CurrentTab = sender as Tab; 
            if (e.Button == MouseButtons.Middle) CloseTab(sender as Tab);
        }

        private void Tab_SizeChanged(object sender, EventArgs e) {
            RearrangeTabs();
        }

        private void Tab_MouseLeave(object sender, EventArgs e) {
            Tab t = sender as Tab;
            if (CurrentTab == t) return;
            t.BackColor = BackColor;
            t.ForeColor = ForeColor;
        }

        private void Tab_MouseEnter(object sender, EventArgs e) {
            Tab t = sender as Tab;
            if (CurrentTab == t) return;
            t.BackColor = Color.FromArgb(200, AccentColor);
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

            foreach (Tab t in Tabs) {
                ToolStripMenuItem item = new ToolStripMenuItem(t.Text, null, (_, evt) => { CurrentTab = t; });
                item.Checked = CurrentTab == t;
                menu.Items.Add(item);
            }

            menu.Show(menuButton, new Point(menuButton.Width - menu.Width, menuButton.Height));
        }

        #endregion
    }
}
