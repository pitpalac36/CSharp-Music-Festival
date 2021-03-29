using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpMusicFestival.validator
{
    class InvalidPurchaseException : Exception
    {
        private new readonly string Message;

        public InvalidPurchaseException(int numberOfTicketsWanted, int showId)
        {
            Message = "Cannot buy " + numberOfTicketsWanted + " tickets for show " + showId + " !!";
        }

        public string getMessage() => Message;
    }
}
