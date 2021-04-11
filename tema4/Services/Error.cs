using System;
using System.Runtime.Serialization;

namespace Services
{
    [Serializable]
    public class Error : Exception
    {
        public Error() { }

        protected Error(SerializationInfo info, StreamingContext context) { }

        public Error(string message) : base(message) { }

        public string GetMessage() => Message;
    }
}