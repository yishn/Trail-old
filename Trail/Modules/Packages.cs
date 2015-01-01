using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Trail.Columns;
using Trail.Controls;
using Trail.DataTypes;
using Trail.Templates;

namespace Trail.Modules {
    public static class Packages {
        public static Dictionary<Tuple<Type, Type>, DragDropAction> DragDropHandlers = new Dictionary<Tuple<Type, Type>, DragDropAction>();

        public static List<Assembly> PackageAssemblies { get; private set; }

        public static void LoadPackages(IHost host) {
            PackageAssemblies = new List<Assembly>();
            if (!Persistence.PackagesFolder.Exists) return;

            foreach (FileInfo file in Persistence.PackagesFolder.EnumerateFiles("*.dll", SearchOption.AllDirectories)) {
                Assembly assembly = Assembly.LoadFrom(file.FullName);
                PackageAssemblies.Add(assembly);

                foreach (Type type in assembly.GetTypes()) {
                    if (!type.IsPublic || type.IsAbstract) continue;
                    if (type.GetInterface(typeof(IPackage).FullName) == null) continue;

                    IPackage package = Activator.CreateInstance(type) as IPackage;
                    package.Initialize(host);
                }
            }
        }

        public static ItemsColumn InstantiateColumn(ColumnData data, IHost host) {
            Type type = Type.GetType(data.ColumnType);
            List<Assembly>.Enumerator enumerable = PackageAssemblies.GetEnumerator();

            while (type == null) {
                if (!enumerable.MoveNext()) return new EmptyColumn(host);
                type = enumerable.Current.GetType(data.ColumnType);
            }

            return Activator.CreateInstance(type, data.Path, host) as ItemsColumn;
        }
    }
}
