using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.Fx {
    public class IntAnimation : Animation {
        private int start, end;

        public new event EventHandler<AnimationValueEventArgs<int>> Tick;

        public IntAnimation() : base() {
            base.Tick += IntAnimation_Tick;
        }
        public IntAnimation(int duration) : base(duration) {
            base.Tick += IntAnimation_Tick;
        }

        private void IntAnimation_Tick(object sender, AnimationValueEventArgs<double> e) {
            int value = start + (int)(e.Value * (end - start));
            if (Tick != null) Tick(this, new AnimationValueEventArgs<int>(value));
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
