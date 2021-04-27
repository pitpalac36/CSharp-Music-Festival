using System;

namespace Networking
{
    public interface Request { }

    [Serializable]
    public class LoginRequest : Request
    {
        private UserDto user;

        public LoginRequest(UserDto user)
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
    public class LogoutRequest : Request
    {
        private UserDto user;

        public LogoutRequest(UserDto user)
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
    public class GetShowsRequest : Request { }


    [Serializable]
    public class GetArtistsByDateRequest : Request
    {
        private string date;

        public GetArtistsByDateRequest(string date)
        {
            this.date = date;
        }

        public virtual string Date
        {
            get
            {
                return date;
            }
        }
    }


    [Serializable]
    public class BuyTicketRequest : Request
    {
        private TicketDto ticketDto;

        public BuyTicketRequest(TicketDto ticketDto)
        {
            this.ticketDto = ticketDto;
        }

        public virtual TicketDto TicketDto
        {
            get
            {
                return ticketDto;
            }
        }
    }


    [Serializable]
    public class GetUserRequest : Request
    {
        private string username;
        private string password;

        public GetUserRequest(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public virtual string Username
        {
            get
            {
                return username;
            }
        }

        public virtual string Password
        {
            get
            {
                return password;
            }
        }
    }
}
