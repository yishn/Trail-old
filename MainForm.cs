using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            DirectoryColumn c = new DirectoryColumn(new DirectoryInfo("D:\\"));
            cvColumns.Columns.Add(c);
            c.RefreshItems();

            LoadDrives();
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            cvColumns.ScrollToEnd();
        }

        public void LoadDrives() {
            TreeNode nodeDrives = tvSide.Nodes["nodeDrives"];
            nodeDrives.Nodes.Clear();

            foreach (DriveInfo dI in DriveInfo.GetDrives()) {
                if (!dI.IsReady) continue;

                TreeNode node = new TreeNode() {
                    Text = dI.VolumeLabel,
                    Tag = dI,
                    ImageKey = dI.DriveType == DriveType.CDRom ? "disc" :
                        dI.DriveType == DriveType.Network ? "network" :
                        dI.DriveType == DriveType.Removable ? "removable" :
                        dI.DriveType == DriveType.Fixed ? "drive" : "unknown"
                };
                node.SelectedImageKey = node.ImageKey;

                nodeDrives.Nodes.Add(node);
            }

            nodeDrives.Expand();
        }
    }
}
