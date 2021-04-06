using System;

namespace Networking
{
    public interface Response { }

    [Serializable]
    public class UpdateResponse : Response { }

    [Serializable]
    public class OkResponse : Response { }

    [Serializable]
    public class ErrorResponse : Response
    {
        private string message;

        public ErrorResponse(string message)
        {
            this.message = message;
        }

        public virtual string Message
        {
            get
            {
                return message;
            }
        }
    }

    [Serializable]
    public class GetShowsResponse : Response
    {
        private ShowDto[] shows;

        public GetShowsResponse(ShowDto[] shows)
        {
            this.shows = shows;
        }

        public virtual ShowDto[] Shows
        {
            get
            {
                return shows;
            }
        }
    }

    [Serializable]
    public class GetArtistsByDateResponse : Response
    {
        private ArtistDto[] artists;

        public GetArtistsByDateResponse(ArtistDto[] artists)
        {
            this.artists = artists;
        }

        public virtual ArtistDto[] Artists
        {
            get
            {
                return artists;
            }
        }
    }


    [Serializable]
    public class GetUserResponse : Response
    {
        private UserDto user;

        public GetUserResponse(UserDto user)
        {
            this.user = user;
        }

        public virtual UserDto User
        {
            get
            {
                return user;
            }
        }
    }


    [Serializable]
    public class BuyTicketResponse : UpdateResponse
    {
        private TicketDto ticket;

        public BuyTicketResponse(TicketDto ticket)
        {
            this.ticket = ticket;
        }

        public virtual TicketDto Ticket
        {
            get
            {
                return ticket;
            }
        }
    }

}
