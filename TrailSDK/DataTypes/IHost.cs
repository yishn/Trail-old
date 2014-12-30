using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Trail.Controls;
using Trail.Templates;

namespace Trail.DataTypes {
    public interface IHost {
        DirectoryInfo PersistenceFolder { get; }
        Dictionary<Tuple<Type, Type>, Action<ItemsColumn, ItemsColumn, ColumnListViewItem[]>> DragDropHandlers { get; }
        Form MainForm { get; }

        void CreatePreference(string key, object value);
        void SetPreference(string key, object value);
        string GetPreference(string key);
        T GetPreference<T>(string key);
        List<string> GetPreferenceList(string key);
        List<T> GetPreferenceList<T>(string key);

        void EnqueueAction(ItemsAction action);
    }
}
