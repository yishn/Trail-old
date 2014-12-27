using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trail.Columns {
    public class ShowErrorException : Exception {
        public ShowErrorException(string message) : base(message) { }
    }
}
