using Services;
using System;
using System.Collections.Generic;
using Model.domain;
using Model.validator;

namespace musicFestival
{
    public class ClientController : MarshalByRefObject, IObserver
    {
        public event EventHandler<UserEventArgs> updateEvent; //ctrl calls it when it has received an update
        private readonly IService server;
        private User currentUser;

        public ClientController(IService server)
        {
            this.server = server;
            currentUser = null;
        }

        protected virtual void OnUserEvent(UserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }

        public void Login(string username, string password)
        {
            User user = new User(username, password);
            server.Login(user, this);
            Console.WriteLine("Login succeeded ....");
            currentUser = user;
            Console.WriteLine("Current user {0}", user);
        }

        public void Logout()
        {
            Console.WriteLine("Controller logout");
            server.Logout(currentUser, this);
            currentUser = null;
        }

        public IList<Show> GetShows()
        {
            Console.WriteLine("Controller GetShows");
            return server.GetAll();
        }

        public IList<Artist> GetArtists(string date)
        {
            Console.WriteLine("Controller GetArtists");
            return server.GetArtists(date);
        }

        public void TicketSold(Ticket ticket)
        {
            Console.WriteLine("Ticket sold " + ticket);
            UserEventArgs userArgs = new UserEventArgs(UserEvent.TicketSold, ticket);
            OnUserEvent(userArgs);
            
        }

        public void SellTicket(Ticket ticket)
        {
            try
            {
                server.SellTickets(ticket);
            }
            catch (InvalidPurchaseException ex)
            {
                throw new Error(ex.GetMessage());
            }
        }
    }
}
