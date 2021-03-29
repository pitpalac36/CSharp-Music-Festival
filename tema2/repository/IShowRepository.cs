using csharpMusicFestival.domain;
using System;
using System.Collections.Generic;

namespace csharpMusicFestival.repository
{
    public interface IShowRepository : IRepository<Show>
    {
        Show FindOne(int id);
        List<ShowDTO> FindArtists(string date);
        void Update(Show show);
    }
}
