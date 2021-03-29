
using csharpMusicFestival.domain;

namespace csharpMusicFestival.repository
{
    interface IUserRepository : IRepository<User>
    {
        bool FindOne(string username, string password);
    }
}
