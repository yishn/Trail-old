using System;
using System.Threading;
using Trail.Controls;
using Trail.DataTypes;

namespace Trail.Templates {
    public abstract class ItemsAction {
        public string HeaderText { get; protected set; }
        public IHost Host { get; private set; }

        public ItemsAction(IHost host) {
            Host = host;
        }

        public abstract void DoWork(IProgress<Tuple<int, string>> progress, CancellationToken token);
    }
}
