using csharpMusicFestival.domain;

namespace csharpMusicFestival.repository
{
    interface ITicketRepository : IRepository<Ticket>
    {
        void Save(Ticket ticket);
    }
}
