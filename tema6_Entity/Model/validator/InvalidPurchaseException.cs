using System;
using System.Runtime.Serialization;

namespace Model.validator
{
    [Serializable]
    public class InvalidPurchaseException : Exception
    {
        public InvalidPurchaseException(int numberOfTicketsWanted, int showId) : base("Cannot buy " + numberOfTicketsWanted + " tickets for show " + showId + " !!") { }
        protected InvalidPurchaseException(SerializationInfo info, StreamingContext context) {}

        public string GetMessage() => base.Message;
    }
}