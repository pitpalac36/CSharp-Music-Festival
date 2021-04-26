using System.Collections.Generic;
using Model.domain;
using Persistance;

namespace Persistence
{
    public interface IShowRepository : IRepository<Show>
    {
        Show FindOne(int id);
        List<Artist> FindArtists(string date);
        void Update(Show show);
    }
}
