using System;
using System.Threading;

namespace Trail.DataTypes {
    public interface IAction {
        string HeaderText { get; }
        IHost Host { get; }

        void DoWork(IProgress<Tuple<int, string>> progress, CancellationToken token);
    }
}
