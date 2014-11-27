using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trail.Controls {
    public class ListViewModern : ListView {
        [DllImport("user32.dll")]
        static private extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public extern static int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        protected override void OnSelectedIndexChanged(EventArgs e) {
            base.OnSelectedIndexChanged(e);
            SendMessage(Handle, 0x127, 0x10001, 0);
        }

        protected override void OnEnter(EventArgs e) {
            base.OnEnter(e);
            SendMessage(Handle, 0x127, 0x10001, 0);
        }
    }
}
