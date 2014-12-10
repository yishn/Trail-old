using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.Modules {
    public class Persistence {
        public DirectoryInfo PersistenceFolder { get; private set; }
        public FileInfo PreferencesFile { get; private set; }

        public Dictionary<string, object> Preferences { get; private set; }

        public Persistence(string persistenceFolder = "") {
            if (persistenceFolder == "") 
                persistenceFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TrailApp");

            this.PersistenceFolder = new DirectoryInfo(persistenceFolder);
            this.PreferencesFile = new FileInfo(Path.Combine(persistenceFolder, "preferences.json"));

            LoadData();
            SaveData();
        }

        public void SaveData() {
            StreamWriter writer = PreferencesFile.CreateText();
            writer.Write(Json.JsonParser.ToJson(Preferences));
            writer.Close();
        }

        public void LoadData() {
            if (!PersistenceFolder.Exists) PersistenceFolder.Create();

            string json = "{}";
            if (PreferencesFile.Exists) {
                StreamReader reader = PreferencesFile.OpenText();
                json = reader.ReadToEnd();
                reader.Close();
            }

            this.Preferences = new Dictionary<string, object>(Json.JsonParser.FromJson(json));
        }
    }
}
