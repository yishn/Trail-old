using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Trail.Controls {
    public class ListViewModern : ListView {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
        [DllImport("user32.dll")]
        private extern static IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public extern static int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public ListViewModern() {
            var doubleBufferPropertyInfo = this.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(this, true, null);
        }

        protected override void CreateHandle() {
            base.CreateHandle();
            SetWindowTheme(this.Handle, "explorer", null);
        }

        protected override void OnSelectedIndexChanged(EventArgs e) {
            base.OnSelectedIndexChanged(e);
            SendMessage(Handle, 0x127, 0x10001, 0);
        }

        protected override void OnEnter(EventArgs e) {
            base.OnEnter(e);
            SendMessage(Handle, 0x127, 0x10001, 0);
        }

        #region Override OnItemActivate

        private bool shiftDown = false;

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.KeyCode == Keys.ShiftKey) shiftDown = true;
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            if (e.KeyCode == Keys.ShiftKey) shiftDown = false;
            base.OnKeyUp(e);
        }

        protected override void OnItemActivate(EventArgs e) {
            if (shiftDown) return;
            base.OnItemActivate(e);
        }

        #endregion
    }
}
