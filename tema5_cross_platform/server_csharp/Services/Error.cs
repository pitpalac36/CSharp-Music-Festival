using System;

namespace Services
{
    public class Error : Exception
    {
        public Error() { }

        public Error(string message) : base(message) { }
    }
}