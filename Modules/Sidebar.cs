using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Columns;
using Trail.Controls;
using Trail.DataTypes;

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
            this.SelectedImageIndex = 0;
            this.ShowLines = false;
            this.ShowNodeToolTips = true;
            this.Size = new Size(196, 382);
        }

        public void Load() {
            ColumnTreeNode node1 = new ColumnTreeNode() {
                Text = "Favorites",
                Name = "favorites",
                ImageKey = "favorites",
                SelectedImageKey = "favorites"
            };
            ColumnTreeNode node2 = new ColumnTreeNode() {
                Text = "Drives",
                Name = "drives",
                ImageKey = "drives",
                SelectedImageKey = "drives"
            };

            this.Nodes.Clear();
            this.Nodes.AddRange(new TreeNode[] {
                node1,
                node2
            });

            LoadDrives();
            LoadFavorites();
        }

        public void LoadDrives() {
            TreeNode drives = this.Nodes["drives"];
            drives.Nodes.Clear();

            foreach (DriveInfo dI in DriveInfo.GetDrives()) {
                if (!dI.IsReady) continue;

                ColumnTreeNode node = new ColumnTreeNode() {
                    Text = dI.VolumeLabel,
                    Tag = dI,
                    SubColumn = new DirectoryColumn(dI.RootDirectory),
                    ImageKey = dI.DriveType == DriveType.CDRom ? "disc" :
                        dI.DriveType == DriveType.Network ? "network" :
                        dI.DriveType == DriveType.Removable ? "removable" :
                        dI.DriveType == DriveType.Fixed ? "drive" : "unknown"
                };
                node.SelectedImageKey = node.ImageKey;

                drives.Nodes.Add(node);
            }

            drives.Expand();
        }

        public void LoadFavorites() {
            TreeNode favorites = this.Nodes["favorites"];
            favorites.Nodes.Clear();

            foreach (FavoriteItem item in Persistence.FavoriteItems) {
                ItemsColumn c = Activator.CreateInstance(Type.GetType(item.ColumnType), item.Path) as ItemsColumn;

                ColumnTreeNode node = new ColumnTreeNode() {
                    Text = c.HeaderText,
                    Tag = item,
                    SubColumn = c,
                    ImageKey = "folder",
                    SelectedImageKey = "folder"
                };

                favorites.Nodes.Add(node);
            }

            favorites.Expand();
        }
    }
}
