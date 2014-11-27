using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.Fx {
    public class AnimationValueEventArgs<T> : EventArgs {
        public T Value { get; set; }

        public AnimationValueEventArgs(T value) {
            this.Value = value;
        }
    }
}
