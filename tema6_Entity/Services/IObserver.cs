using Model.domain;

namespace Services
{
    public interface IObserver
    {
        void TicketSold(Ticket ticket);
    }
}
