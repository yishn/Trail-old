﻿using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Trail.Controls {
    public class TreeViewModern : TreeView {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
        [DllImport("user32.dll")]
        private extern static IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public extern static int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public TreeViewModern() {
            HotTracking = true;
            var doubleBufferPropertyInfo = GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(this, true, null);
        }

        protected override void CreateHandle() {
            base.CreateHandle();
            SetWindowTheme(Handle, "explorer", null);
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
