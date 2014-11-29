using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Controls;

namespace Trail.Modules {
    public class Sidebar : TreeViewModern {
        public Sidebar() {
            this.BackColor = SystemColors.Control;
            this.BorderStyle = BorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.ImageIndex = 0;
            this.Indent = 19;
            this.ItemHeight = 22;
            this.Location = new Point(0, 0);

            TreeNode node1 = new TreeNode("Bookmarks") { 
                Name = "bookmarks",
                ImageKey = "bookmarks",
                SelectedImageKey = "bookmarks"
            };
            TreeNode node2 = new TreeNode("Drives") { 
                Name = "drives",
                ImageKey = "drives",
                SelectedImageKey = "drives"
            };

            this.Nodes.AddRange(new TreeNode[] {
                node1,
                node2
            });

            this.SelectedImageIndex = 0;
            this.ShowLines = false;
            this.ShowNodeToolTips = true;
            this.Size = new Size(196, 382);
        }

        public void LoadDrives() {
            TreeNode nodeDrives = this.Nodes["drives"];
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
