using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.DataTypes;

namespace Trail.Templates {
    public interface IPackage {
        string Name { get; }
        string Author { get; }
        string Website { get; }
        string Description { get; }
        string Version { get; }

        void Initialize(IHost host);
    }
}
