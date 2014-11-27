using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public class TreeViewModern : TreeView {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
        [DllImport("user32.dll")]
        private extern static IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public extern static int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        protected override void CreateHandle() {
            base.CreateHandle();
            SetWindowTheme(this.Handle, "explorer", null);
        }

        protected override void OnAfterSelect(TreeViewEventArgs e) {
            base.OnAfterSelect(e);
            SendMessage(Handle, 0x127, 0x10001, 0);
        }

        protected override void OnEnter(EventArgs e) {
            base.OnEnter(e);
            SendMessage(Handle, 0x127, 0x10001, 0);
        }
    }
}
