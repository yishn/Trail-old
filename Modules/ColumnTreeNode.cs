using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trail.Columns;

namespace Trail.Modules {
    public class ColumnTreeNode : TreeNode {
        public ItemsColumn SubColumn { get; set; }
    }
}
