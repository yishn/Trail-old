using System.Windows.Forms;

namespace Trail.Controls {
    public class FlatButton : Button {
        public FlatButton() {
            SetStyle(ControlStyles.Selectable, false);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }
    }
}
