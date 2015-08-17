using System.Reflection;
using System.Windows.Forms;

namespace Trail.Controls {
    public class SplitContainerModern : SplitContainer {
        public SplitContainerModern() {
            MouseDown += SplitContainerModern_MouseDown;
            MouseUp += SplitContainerModern_MouseUp;
            MouseMove += SplitContainerModern_MouseMove;

            var doubleBufferPropertyInfo = GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(this, true, null);
        }

        private void SplitContainerModern_MouseDown(object sender, MouseEventArgs e) {
            // This disables the normal move behavior
            IsSplitterFixed = true;
        }

        private void SplitContainerModern_MouseUp(object sender, MouseEventArgs e) {
            // This allows the splitter to be moved normally again
            IsSplitterFixed = false;
        }

        private void SplitContainerModern_MouseMove(object sender, MouseEventArgs e) {
            // Check to make sure the splitter won't be updated by the
            // normal move behavior also
            if (IsSplitterFixed) {
                // Make sure that the button used to move the splitter
                // is the left mouse button
                if (e.Button.Equals(MouseButtons.Left)) {
                    // Checks to see if the splitter is aligned Vertically
                    if (Orientation.Equals(Orientation.Vertical)) {
                        // Only move the splitter if the mouse is within
                        // the appropriate bounds
                        if (e.X > 0 && e.X < Width) {
                            // Move the splitter & force a visual refresh
                            SplitterDistance = e.X;
                            Refresh();
                        }
                    } else {
                        // Only move the splitter if the mouse is within
                        // the appropriate bounds
                        if (e.Y > 0 && e.Y < Height) {
                            // Move the splitter & force a visual refresh
                            SplitterDistance = e.Y;
                            Refresh();
                        }
                    }
                } else {
                    // This allows the splitter to be moved normally again
                    IsSplitterFixed = false;
                }
            }
        }
    }
}
