using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.Modules {
    public class Persistence {
        public DirectoryInfo PersistenceFolder { get; set; }
        public FileInfo PreferencesFile { get; set; }

        public Persistence() {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TrailApp");
            this.PersistenceFolder = new DirectoryInfo(folderPath);
            this.PreferencesFile = new FileInfo(Path.Combine(folderPath, "preferences.json"));

            Initialization();
        }

        private void Initialization() {
            if (!PersistenceFolder.Exists) PersistenceFolder.Create();
            if (!PreferencesFile.Exists) PreferencesFile.Create();
        }
    }
}
