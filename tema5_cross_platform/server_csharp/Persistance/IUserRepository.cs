
using csharpMusicFestival.domain;

namespace csharpMusicFestival.repository
{
    public interface IUserRepository : IRepository<User>
    {
        bool FindOne(string username, string password);
    }
}
