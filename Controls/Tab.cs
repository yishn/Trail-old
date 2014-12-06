using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public partial class Tab : UserControl {
        private bool _autoHideClose = true;

        public override string Text { get { return lblText.Text; } set { lblText.Text = value; } }
        public bool AutoHideClose {
            get { return _autoHideClose; }
            set {
                _autoHideClose = value;
                btnClose.Visible = !_autoHideClose;
            }
        }

        public event EventHandler CloseButtonClick;

        public Tab() {
            InitializeComponent();

            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 255, 255, 255);
            btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0, 0);
        }

        private void Tab_Load(object sender, EventArgs e) {
            lblText_SizeChanged(sender, e);
        }

        private void lblText_SizeChanged(object sender, EventArgs e) {
            this.Width = lblText.Width + btnClose.Width + 10;
            btnClose.Left = lblText.Right + 5;
        }

        #region Mouse Enter & Leave

        protected override void OnMouseLeave(EventArgs e) {
            if (this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
                return;
            else {
                base.OnMouseLeave(e);
            }
        }

        private void btnClose_MouseLeave(object sender, EventArgs e) {
            OnMouseLeave(e);
        }

        private void btnClose_MouseEnter(object sender, EventArgs e) {
            OnMouseEnter(e);
        }

        private void Tab_MouseEnter(object sender, EventArgs e) {
            btnClose.Visible = true;
        }

        private void Tab_MouseLeave(object sender, EventArgs e) {
            if (AutoHideClose) btnClose.Visible = false;
        }

        #endregion

        private void lblText_Click(object sender, EventArgs e) {
            OnClick(e);
        }

        private void lblText_MouseClick(object sender, MouseEventArgs e) {
            OnMouseClick(e);
        }

        private void btnClose_Click(object sender, EventArgs e) {
            if (CloseButtonClick != null) CloseButtonClick(this, e);
        }
    }
}
