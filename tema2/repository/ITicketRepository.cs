using csharpMusicFestival.domain;

namespace csharpMusicFestival.repository
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        void Save(Ticket ticket);
    }
}
