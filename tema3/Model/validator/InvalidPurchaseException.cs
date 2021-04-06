using System;

namespace csharpMusicFestival.validator
{
    public class InvalidPurchaseException : Exception
    {
        private new readonly string Message;

        public InvalidPurchaseException(int numberOfTicketsWanted, int showId) : base("Cannot buy " + numberOfTicketsWanted + " tickets for show " + showId + " !!") {}

        public string getMessage() => Message;
    }
}