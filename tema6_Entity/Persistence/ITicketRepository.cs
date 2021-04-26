
using Model.domain;
using Persistance;

namespace Persistence
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        void Save(Ticket ticket);
    }
}
