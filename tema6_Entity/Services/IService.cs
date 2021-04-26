using Model.domain;

namespace Services
{
    public interface IService
    {
        void Login(User user, IObserver client);
        void Logout(User user, IObserver client);
        Show[] GetAll();
        Artist[] GetArtists(string date);
        void SellTickets(Ticket ticket);
    }
}
