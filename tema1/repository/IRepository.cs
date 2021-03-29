using System.Collections.Generic;


namespace csharpMusicFestival.repository
{
    interface IRepository<T>
    {
        List<T> FindAll();
    }
}
