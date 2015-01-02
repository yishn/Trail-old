using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trail.DataTypes;
using Trail.Templates;

namespace Trail.Actions {
    public class DirectoryDeleteAction : ItemsAction {
        public string[] Items { get; private set; }
        public bool Recycle { get; private set; }

        public DirectoryDeleteAction(string[] items, IHost host, bool recycle = true) : base(host) {
            this.HeaderText = (recycle ? "Recycle" : "Delete") + " files";
            this.Items = items;
            this.Recycle = recycle;
        }

        public override void DoWork(IProgress<Tuple<int, string>> progress, CancellationToken token) {
            for (int i = 0; i < Items.Length; i++) {
                token.ThrowIfCancellationRequested();

                int percentage = i * 100 / Items.Length;
                progress.Report(new Tuple<int, string>(percentage, Path.GetFileName(Items[i])));

                string path = Items[i];
                RecycleOption option = Recycle ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently;

                if (Directory.Exists(path))
                    FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, option);
                else if (File.Exists(path))
                    FileSystem.DeleteFile(path, UIOption.OnlyErrorDialogs, option);
            }
        }
    }
}
