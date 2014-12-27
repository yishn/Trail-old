using System.Windows.Forms;

namespace Trail.Controls {
    public class FlatButton : Button {
        public FlatButton() {
            this.SetStyle(ControlStyles.Selectable, false);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }
    }
}
