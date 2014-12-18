using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.Controls {
    public class FlatButton : Button {
        public FlatButton() {
            this.SetStyle(ControlStyles.Selectable, false);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }
    }
}
