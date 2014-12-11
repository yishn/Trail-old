﻿using System;
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

        public static void LoadData() {
            if (!PersistenceFolder.Exists) PersistenceFolder.Create();

            string json = "{}";
            if (PreferencesFile.Exists) {
                StreamReader reader = PreferencesFile.OpenText();
                json = reader.ReadToEnd();
                reader.Close();
            }

            Preferences = new Dictionary<string, object>(Json.JsonParser.FromJson(json));
            createDefaultData();
        }

        public static void SaveData() {
            StreamWriter writer = PreferencesFile.CreateText();
            writer.Write(Json.JsonParser.ToJson(Preferences));
            writer.Close();
        }

        public static void SetPreference(string key, object value) {
            Preferences[key] = value;
            SaveData();
        }

        public static string GetPreferenceString(string key) {
            return Preferences[key].ToString();
        }

        public static List<string> GetPreferenceList(string key) {
            return (Preferences[key] as List<object>).Select(x => x.ToString()).ToList();
        }

        public static void CreatePreference(string key, object value) {
            if (Preferences.ContainsKey(key)) return;
            Preferences[key] = value;
        }

        private static void createDefaultData() {
            CreatePreference("window.size", new List<object>(new object[] { 850, 413 }));
            CreatePreference("column.folder_exclude_patterns", new List<string>(new string[] { 
                "$RECYCLE.BIN", ".*", "System Volume Information"
            }));
            CreatePreference("column.file_exclude_patterns", new List<string>(new string[] { 
                "desktop.ini", ".*"
            }));
            CreatePreference("column.DirectoryColumn.individual_icon_files", new List<string>(new string[] { 
                "*.exe", "*.ico"
            }));
            SaveData();
        }
    }
}
