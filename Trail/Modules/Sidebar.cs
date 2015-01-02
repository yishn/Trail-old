using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Trail.Columns;
using Trail.Controls;
using Trail.DataTypes;
using Trail.Helpers;
using Trail.Templates;

namespace Trail.Modules {
    public class Sidebar : TreeViewModern {
        public IHost Host { get { return this.FindForm() as IHost; } }

        public Sidebar() {
            this.BackColor = SystemColors.Control;
            this.BorderStyle = BorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.FullRowSelect = true;
            this.ImageIndex = 0;
            this.Indent = 19;
            this.ItemHeight = 22;
            this.Location = new Point(0, 0);
            this.SelectedImageIndex = 0;
            this.ShowLines = false;
            this.ShowNodeToolTips = true;
            this.Size = new Size(196, 382);

            this.LostFocus += Sidebar_LostFocus;
        }

        public void Load() {
            this.ImageList.Images.Add("favorites", FugueIcons.Star);
            this.ImageList.Images.Add("devices", FugueIcons.Computer);
            this.ImageList.Images.Add("drive", FugueIcons.Drive);
            this.ImageList.Images.Add("unknown", FugueIcons.DriveExclamation);
            this.ImageList.Images.Add("disc", FugueIcons.DriveDiscBlue);
            this.ImageList.Images.Add("network", FugueIcons.DriveNetwork);
            this.ImageList.Images.Add("removable", FugueIcons.DriveArrow);

            ColumnTreeNode node1 = new ColumnTreeNode() {
                Text = "Favorites",
                Name = "favorites",
                ImageKey = "favorites",
                SelectedImageKey = "favorites"
            };
            ColumnTreeNode node2 = new ColumnTreeNode() {
                Text = "Devices",
                Name = "devices",
                ImageKey = "devices",
                SelectedImageKey = "devices"
            };

            this.Nodes.Clear();
            this.Nodes.AddRange(new TreeNode[] {
                node1,
                node2
            });

            LoadDevices();
            LoadFavorites();
        }

        public void LoadDevices() {
            TreeNode drives = this.Nodes["devices"];
            drives.Nodes.Clear();

            foreach (DriveInfo dI in DriveInfo.GetDrives()) {
                if (!dI.IsReady) continue;

                ColumnData data = new ColumnData(typeof(DirectoryColumn).FullName, dI.RootDirectory.FullName);
                drives.Nodes.Add(getNodeFrom(data));
            }

            drives.Expand();
        }

        public void LoadFavorites() {
            TreeNode favorites = this.Nodes["favorites"];
            favorites.Nodes.Clear();

            foreach (ColumnData data in Persistence.FavoriteItems) {
                favorites.Nodes.Add(getNodeFrom(data));
            }

            favorites.Expand();
        }

        private ColumnTreeNode getNodeFrom(ColumnData data) {
            ItemsColumn column = Packages.InstantiateColumn(data, Host);
            string key = Guid.NewGuid().ToString();

            this.ImageList.Images.Add(key, column.GetIcon());
            return new ColumnTreeNode() {
                Text = column.GetHeaderText(),
                SubColumn = data,
                ImageKey = key,
                SelectedImageKey = key
            };
        }

        private void Sidebar_LostFocus(object sender, EventArgs e) {
            if (this.SelectedNode == null) return;
            this.SelectedNode = this.SelectedNode.Parent ?? this.SelectedNode;
        }
    }
}
