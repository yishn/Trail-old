﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.DataTypes {
    public interface IPersistence {
        void SetPreference(string key, object value);
        string GetPreference(string key);
        T GetPreference<T>(string key);
        List<string> GetPreferenceList(string key);
        List<T> GetPreferenceList<T>(string key);
    }
}
