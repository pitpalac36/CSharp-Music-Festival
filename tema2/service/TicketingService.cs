using csharpMusicFestival.domain;
using csharpMusicFestival.repository;
using csharpMusicFestival.validator;
using System;
using System.Collections.Generic;

namespace csharpMusicFestival.service
{
    public class TicketingService
    {
        private IShowRepository showRepo;
        private ITicketRepository ticketRepo;

        public TicketingService(IShowRepository showRepo, ITicketRepository ticketRepo)
        {
            this.showRepo = showRepo;
            this.ticketRepo = ticketRepo;
        }

        public List<Show> GetAll()
        {
            List<Show> all = showRepo.FindAll();
            return all;
        }
        public List<ShowDTO> GetArtists(string date)
        {
            return showRepo.FindArtists(date);
        }

        public void BuyTickets(int showId, int quantity, String purchaserName)
        {
            Show show = showRepo.FindOne(showId);
            if (show.AvailableTicketsNumber < quantity) 
            {
                throw new InvalidPurchaseException(quantity, showId);
            } 
            else 
            {
                ticketRepo.Save(new Ticket(showId, purchaserName, quantity));
                show.AvailableTicketsNumber = show.AvailableTicketsNumber - quantity;
                show.SoldTicketsNumber = show.SoldTicketsNumber + quantity;
                showRepo.Update(show);
             }
            }
        }
    }
