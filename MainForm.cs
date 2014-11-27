using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;

namespace Trail {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            for (int i = 0; i < 10; i++) {
                cvColumns.Columns.Add(new ColumnControl() { HeaderText = "Hello World " + i });
            }

            foreach (ColumnControl c in cvColumns.Columns) {
                for (int i = 0; i < 10; i++) {
                    c.Items.Add(new ListViewItem("blah " + i));
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            cvColumns.ScrollToEnd();
        }
    }
}
