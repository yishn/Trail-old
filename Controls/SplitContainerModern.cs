using System.Reflection;
using System.Windows.Forms;

namespace Trail.Controls {
    public class SplitContainerModern : SplitContainer {
        public SplitContainerModern() {
            this.MouseDown += SplitContainerModern_MouseDown;
            this.MouseUp += SplitContainerModern_MouseUp;
            this.MouseMove += SplitContainerModern_MouseMove;

            var doubleBufferPropertyInfo = this.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(this, true, null);
        }

        private void SplitContainerModern_MouseDown(object sender, MouseEventArgs e) {
            // This disables the normal move behavior
            this.IsSplitterFixed = true;
        }

        private void SplitContainerModern_MouseUp(object sender, MouseEventArgs e) {
            // This allows the splitter to be moved normally again
            this.IsSplitterFixed = false;
        }

        private void SplitContainerModern_MouseMove(object sender, MouseEventArgs e) {
            // Check to make sure the splitter won't be updated by the
            // normal move behavior also
            if (this.IsSplitterFixed) {
                // Make sure that the button used to move the splitter
                // is the left mouse button
                if (e.Button.Equals(MouseButtons.Left)) {
                    // Checks to see if the splitter is aligned Vertically
                    if (this.Orientation.Equals(Orientation.Vertical)) {
                        // Only move the splitter if the mouse is within
                        // the appropriate bounds
                        if (e.X > 0 && e.X < this.Width) {
                            // Move the splitter & force a visual refresh
                            this.SplitterDistance = e.X;
                            this.Refresh();
                        }
                    } else {
                        // Only move the splitter if the mouse is within
                        // the appropriate bounds
                        if (e.Y > 0 && e.Y < this.Height) {
                            // Move the splitter & force a visual refresh
                            this.SplitterDistance = e.Y;
                            this.Refresh();
                        }
                    }
                } else {
                    // This allows the splitter to be moved normally again
                    this.IsSplitterFixed = false;
                }
            }
        }
    }
}
