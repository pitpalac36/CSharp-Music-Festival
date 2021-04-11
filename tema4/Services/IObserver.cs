using csharpMusicFestival.domain;

namespace Services
{
    public interface IObserver
    {
        void TicketSold(Ticket ticket);
    }
}
