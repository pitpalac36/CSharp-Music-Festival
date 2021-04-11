using System;
using System.Runtime.Serialization;

namespace csharpMusicFestival.validator
{
    [Serializable]
    public class InvalidPurchaseException : Exception
    {
        private new readonly string Message;

        public InvalidPurchaseException(int numberOfTicketsWanted, int showId) : base("Cannot buy " + numberOfTicketsWanted + " tickets for show " + showId + " !!") {}

        protected InvalidPurchaseException(SerializationInfo info, StreamingContext context) {}

        public string GetMessage() => Message;
    }
}