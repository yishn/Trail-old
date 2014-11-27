using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trail.Fx {
    public class Animation : Timer {
        private int timeCounter;

        public int Duration { get; set; }
        public Func<double, double> AnimationFunction { get; set; }

        public new event EventHandler<AnimationValueEventArgs<double>> Tick;
        public event EventHandler Complete;

        public Animation() : this(15) { }
        public Animation(int duration) {
            this.AnimationFunction = (t) => Math.Pow(Math.Sin(Math.PI / 2 * t), 3);

            this.Duration = duration;
            this.Enabled = false;
            this.Interval = 1;
            base.Tick += new EventHandler(Animation_Tick);
        }

        private void Animation_Tick(object sender, EventArgs e) {
            if (Duration <= 0) {
                Complete(this, new EventArgs());
                return;
            }

            double time = 1.0 * timeCounter / Duration;
            double value = AnimationFunction(time);
            if (Tick != null) Tick(this, new AnimationValueEventArgs<double>(value));

            if (timeCounter == Duration) {
                this.Enabled = false;
                if (Complete != null) Complete(this, new EventArgs());

                return;
            }

            timeCounter++;
        }

        public new Animation Start() {
            if (this.Enabled) return this;
            this.timeCounter = 0;
            this.Enabled = true;

            return this;
        }

        public new Animation Stop() {
            this.Enabled = false;
            return this;
        }
    }
}
