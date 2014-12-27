using System;

namespace Trail.Columns {
    public class ShowErrorException : Exception {
        public ShowErrorException(string message) : base(message) { }
    }
}
