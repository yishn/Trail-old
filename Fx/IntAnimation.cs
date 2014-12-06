using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.Fx {
    public class IntAnimation : Animation {
        private int start, end;

        public new event EventHandler<int> Tick;

        public IntAnimation() : base() {
            base.Tick += IntAnimation_Tick;
        }
        public IntAnimation(int duration) : base(duration) {
            base.Tick += IntAnimation_Tick;
        }

        private void IntAnimation_Tick(object sender, double e) {
            int value = start + (int)(e * (end - start));
            if (Tick != null) Tick(this, value);
        }

        public new IntAnimation Start() { return this.Start(0, 100); }
        public IntAnimation Start(int start, int end) {
            this.start = start;
            this.end = end;
            base.Start();

            return this;
        }
    }
}
