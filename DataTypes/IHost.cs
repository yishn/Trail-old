using System;
using System.Collections.Generic;
using Trail.Actions;
using Trail.Columns;

namespace Trail.DataTypes {
    public interface IHost {
        Dictionary<Tuple<string, string>, Action<ItemsColumn, ItemsColumn, ColumnListViewItem[]>> DragDropHandlers { get; }

        void CreatePreference(string key, object value);
        void SetPreference(string key, object value);
        string GetPreference(string key);
        T GetPreference<T>(string key);
        List<string> GetPreferenceList(string key);
        List<T> GetPreferenceList<T>(string key);

        void EnqueueAction(IAction action);
    }
}
