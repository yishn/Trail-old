﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trail.Templates;

namespace Trail.DataTypes {
    public class ColumnData {
        public string ColumnType { get; set; }
        public string Path { get; set; }

        public ColumnData() { }

        public ColumnData(string columnType, string path) {
            ColumnType = columnType;
            Path = path;
        }
    }
}
