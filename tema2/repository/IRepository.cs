using System.Collections.Generic;


namespace csharpMusicFestival.repository
{
    public interface IRepository<T>
    {
        List<T> FindAll();
    }
}
