using csharpMusicFestival.domain;
using csharpMusicFestival.validator;
using csharpMusicFestival.repository;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server
{
    public class Service : IService
    {
        private IUserRepository userRepository;
        private ITicketRepository ticketRepository;
        private IShowRepository showRepository;
        private IDictionary<string, IObserver> loggedClients;

        public Service(IUserRepository userRepo, ITicketRepository ticketRepo, IShowRepository showRepo)
        {
            userRepository = userRepo;
            ticketRepository = ticketRepo;
            showRepository = showRepo;
            loggedClients = new Dictionary<string, IObserver>();
        }

        public void Login(User user, IObserver client)
        {
            if (userRepository.FindOne(user.Name, user.Password))
            {
                IObserver output;
                loggedClients.TryGetValue(user.Name, out output);
                if (output != null)
                {
                    throw new Error("User already logged in!");
                }
                loggedClients.Add(user.Name, client);
            }
            else
            {
                throw new Error("Authentication failed!");
            }
        }

        public void Logout(User user, IObserver client)
        {
            IObserver output;
            loggedClients.TryGetValue(user.Name, out output);

            if (output == null)
            {
                throw new Error("User " + user.Name + " was not logged in!");
            }
        }

        public Show[] GetAll()
        {
            return showRepository.FindAll().ToArray();
        }

        public Artist[] GetArtists(string date)
        {
            return showRepository.FindArtists(date).ToArray();
        }

        public void SellTickets(Ticket ticket)
        {
            Show show = showRepository.FindOne(ticket.ShowId);
            if (show.AvailableTicketsNumber < ticket.Number)
            {
                throw new InvalidPurchaseException(ticket.Number, ticket.ShowId);
            }
            else
            {
                ticketRepository.Save(ticket);
                show.AvailableTicketsNumber = show.AvailableTicketsNumber - ticket.Number;
                show.SoldTicketsNumber = show.SoldTicketsNumber + ticket.Number;
                showRepository.Update(show);
                Console.WriteLine("Notifying others about the sell");
                foreach (var each in loggedClients)
                {
                    Console.WriteLine("Notifying user " + each.Key);
                    Task.Run(() => each.Value.TicketSold(ticket));
                }
            }
        }
    }
}
