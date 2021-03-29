using csharpMusicFestival.domain;
using System;
using System.Collections.Generic;

namespace csharpMusicFestival.repository
{
    interface IShowRepository : IRepository<Show>
    {
        Show FindOne(int id);
        List<Show> FindArtists(DateTime date);
        void Update(Show show);
    }
}
