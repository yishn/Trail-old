using System;

namespace Trail.DataTypes {
    public class ShowErrorException : Exception {
        public ShowErrorException(string message) : base(message) { }
    }
}
