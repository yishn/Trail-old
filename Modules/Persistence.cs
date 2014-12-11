using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Trail.Modules {
    public static class Persistence {
        public static DirectoryInfo PersistenceFolder 
            = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TrailApp"));
        public static FileInfo PreferencesFile 
            = new FileInfo(Path.Combine(PersistenceFolder.FullName, "preferences.json"));

        public static Dictionary<string, object> Preferences { get; private set; }

        public static void SaveData() {
            StreamWriter writer = PreferencesFile.CreateText();
            writer.Write(Json.JsonParser.ToJson(Preferences));
            writer.Close();
        }

        public static void LoadData() {
            if (!PersistenceFolder.Exists) PersistenceFolder.Create();

            string json = "{}";
            if (PreferencesFile.Exists) {
                StreamReader reader = PreferencesFile.OpenText();
                json = reader.ReadToEnd();
                reader.Close();
            }

            Persistence.Preferences = new Dictionary<string, object>(Json.JsonParser.FromJson(json));
        }

        public static string GetPreference(string key) {
            return GetPreference<string>(key);
        }
        public static T GetPreference<T>(string key) {
            return (T)Persistence.Preferences[key];
        }

        public static void CreatePreference(string key, object value) {
            if (Persistence.Preferences.ContainsKey(key)) return;
            Persistence.Preferences[key] = value;
        }
    }
}
