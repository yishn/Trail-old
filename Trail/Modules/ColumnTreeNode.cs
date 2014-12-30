using System.Windows.Forms;
using Trail.DataTypes;

namespace Trail.Modules {
    public class ColumnTreeNode : TreeNode {
        public ColumnData SubColumn { get; set; }
    }
}
