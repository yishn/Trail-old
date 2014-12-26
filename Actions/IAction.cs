using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trail.Actions {
    public interface IAction {
        string HeaderText { get; }

        void DoWork(IProgress<Tuple<int, string>> progress, CancellationToken token);
    }
}
