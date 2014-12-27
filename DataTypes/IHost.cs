using System.Collections.Generic;
using Trail.Actions;

namespace Trail.DataTypes {
    public interface IHost {
        void SetPreference(string key, object value);
        string GetPreference(string key);
        T GetPreference<T>(string key);
        List<string> GetPreferenceList(string key);
        List<T> GetPreferenceList<T>(string key);

        void EnqueueAction(IAction action);
    }
}
