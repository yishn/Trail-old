using System;
using System.Threading;
using Trail.DataTypes;

namespace Trail.Actions {
    public interface IAction {
        string HeaderText { get; }
        IHost Host { get; }

        void DoWork(IProgress<Tuple<int, string>> progress, CancellationToken token);
    }
}
