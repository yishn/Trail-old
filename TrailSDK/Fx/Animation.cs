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

        public new event EventHandler<double> Tick;
        public event EventHandler Complete;

        public Animation() : this(15) { }
        public Animation(int duration) {
            AnimationFunction = t => Math.Pow(Math.Sin(Math.PI / 2 * t), 3);

            Duration = duration;
            Enabled = false;
            Interval = 1;
            base.Tick += new EventHandler(Animation_Tick);
        }

        private void Animation_Tick(object sender, EventArgs e) {
            if (Duration <= 0) {
                Complete(this, new EventArgs());
                return;
            }

            double time = 1.0 * timeCounter / Duration;
            double value = AnimationFunction(time);
            if (Tick != null) Tick(this, value);

            if (timeCounter == Duration) {
                Enabled = false;
                if (Complete != null) Complete(this, new EventArgs());

                return;
            }

            timeCounter++;
        }

        public new Animation Start() {
            if (Enabled) return this;
            timeCounter = 0;
            Enabled = true;

            return this;
        }

        public new Animation Stop() {
            Enabled = false;
            return this;
        }

        public Animation End() {
            Stop();
            if (Tick != null) Tick(this, 1.0);
            return this;
        }
    }
}
