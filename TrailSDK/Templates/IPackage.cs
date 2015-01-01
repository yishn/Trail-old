using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.DataTypes;

namespace TrailSDK.Templates {
    public interface IPackage {
        string Name { get; }
        string Author { get; }
        string Website { get; }
        string Description { get; }

        void Initialize(IHost host);
    }
}
