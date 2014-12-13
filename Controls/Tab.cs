using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Trail.Controls {
    public partial class Tab : UserControl {
        private bool autoHideClose = true;

        public override string Text { 
            get { return lblText.Text; }
            set { 
                lblText.Text = value;
                OnTextChanged(new EventArgs());
            } 
        }
        public bool AutoHideClose {
            get { return autoHideClose; }
            set {
                autoHideClose = value;
                btnClose.Visible = !autoHideClose;
            }
        }

        public event EventHandler CloseButtonClick;

        public Tab() {
            InitializeComponent();

            var doubleBufferPropertyInfo = this.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(this, true, null);

            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 255, 255, 255);
            btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 0, 0);

            this.TextChanged += Tab_TextChanged;
        }

        private void Tab_TextChanged(object sender, EventArgs e) {
            this.Width = lblText.PreferredWidth + btnClose.Width + 10;
            lblText.Width = this.Width - btnClose.Width - 3;
            btnClose.Left = this.Width - btnClose.Width - 3;
        }

        #region Mouse Enter & Leave

        protected override void OnMouseLeave(EventArgs e) {
            if (this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
                return;
            else {
                base.OnMouseLeave(e);
            }
        }

        private void Tab_MouseEnter(object sender, EventArgs e) {
            btnClose.Visible = true;
        }

        private void Tab_MouseLeave(object sender, EventArgs e) {
            if (AutoHideClose) btnClose.Visible = false;
        }

        private void Control_MouseEnter(object sender, EventArgs e) {
            OnMouseEnter(e);
        }

        private void Control_MouseLeave(object sender, EventArgs e) {
            OnMouseLeave(e);
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e) {
            if (CloseButtonClick != null) CloseButtonClick(this, e);
        }

        #region Redirect events

        private void lblText_Click(object sender, EventArgs e) {
            OnClick(e);
        }

        private void lblText_MouseClick(object sender, MouseEventArgs e) {
            OnMouseClick(e);
        }

        private void lblText_MouseDown(object sender, MouseEventArgs e) {
            OnMouseDown(e);
        }

        private void lblText_MouseMove(object sender, MouseEventArgs e) {
            OnMouseMove(e);
        }

        private void lblText_MouseUp(object sender, MouseEventArgs e) {
            OnMouseUp(e);
        }

        #endregion
    }
}
