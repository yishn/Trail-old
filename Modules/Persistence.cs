using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Trail.DataTypes;

namespace Trail.Modules {
    public static class Persistence {
        public static DirectoryInfo PersistenceFolder
            = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TrailApp"));
        public static FileInfo PreferencesFile
            = new FileInfo(Path.Combine(PersistenceFolder.FullName, "preferences.json"));
        public static FileInfo FavoritesFile
            = new FileInfo(Path.Combine(PersistenceFolder.FullName, "favorites.json"));

        public static Dictionary<string, object> Preferences { get; private set; }
        public static List<FavoriteItem> FavoriteItems { get; private set; }

        public static void LoadData() {
            if (!PersistenceFolder.Exists) PersistenceFolder.Create();

            string json = "{}";
            if (PreferencesFile.Exists) {
                StreamReader reader = PreferencesFile.OpenText();
                json = reader.ReadToEnd();
                reader.Close();
            }
            Preferences = new Dictionary<string, object>(Json.JsonParser.FromJson(json));

            json = "[]";
            if (FavoritesFile.Exists) {
                StreamReader reader = FavoritesFile.OpenText();
                json = reader.ReadToEnd();
                reader.Close();
            }
            FavoriteItems = Json.JsonParser.DeserializeList<FavoriteItem>(json);

            createDefaultData();
        }

        public static void SaveData() {
            StreamWriter writer = PreferencesFile.CreateText();
            writer.Write(Json.JsonParser.ToJson(Preferences));
            writer.Close();

            writer = FavoritesFile.CreateText();
            writer.Write(Json.JsonParser.SerializeList<FavoriteItem>(FavoriteItems));
            writer.Close();
        }

        public static void SetPreference(string key, object value) {
            Preferences[key] = value;
            SaveData();
        }

        public static string GetPreference(string key) {
            return Preferences[key].ToString();
        }
        public static T GetPreference<T>(string key) {
            return (T)Preferences[key];
        }

        public static List<string> GetPreferenceList(string key) {
            return (Preferences[key] as List<object>).Select(x => x.ToString()).ToList();
        }
        public static List<T> GetPreferenceList<T>(string key) {
            return (Preferences[key] as List<object>).Select(x => (T)x).ToList();
        }

        public static void CreatePreference(string key, object value) {
            if (Preferences.ContainsKey(key)) return;
            Preferences[key] = value;
        }

        private static void createDefaultData() {
            CreatePreference("window.size", new List<object>(new object[] { 850, 413 }));
            CreatePreference("directorycolumn.directory_exclude_patterns", new List<object>(new object[] { 
                "$RECYCLE.BIN", ".*", "System Volume Information"
            }));
            CreatePreference("directorycolumn.file_exclude_patterns", new List<object>(new object[] { 
                "desktop.ini", ".*"
            }));
            CreatePreference("directorycolumn.individual_icon_files", new List<object>(new object[] { 
                "*.exe", "*.ico", "*.lnk", "*.msi"
            }));
            CreatePreference("sidebar.width", 196);

            SaveData();
        }
    }
}
