using System;
using System.Threading;

namespace Trail.Actions {
    public interface IAction {
        string HeaderText { get; }

        void DoWork(IProgress<Tuple<int, string>> progress, CancellationToken token);
    }
}
