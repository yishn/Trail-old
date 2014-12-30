using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.Fx {
    public class ColorAnimation : Animation {
        private Color start, end;

        public new event EventHandler<Color> Tick;

        public ColorAnimation() : base() {
            base.Tick += ColorAnimation_Tick;
        }
        public ColorAnimation(int duration) : base(duration) {
            base.Tick += ColorAnimation_Tick;
        }

        private void ColorAnimation_Tick(object sender, double e) {
            int red = start.R + (int)((end.R - start.R) * e);
            int green = start.G + (int)((end.G - start.G) * e);
            int blue = start.B + (int)((end.B - start.B) * e);
            int alpha = start.A + (int)((end.A - start.A) * e);

            Color value = Color.FromArgb(alpha, red, green, blue);
            if (Tick != null) Tick(this, value);
        }

        public new ColorAnimation Start() { return this.Start(Color.Black, Color.White); }
        public ColorAnimation Start(Color start, Color end) {
            this.start = start;
            this.end = end;
            base.Start();

            return this;
        }
    }
}
