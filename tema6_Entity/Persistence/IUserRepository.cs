
using Model.domain;
using Persistance;

namespace Persistence
{
    public interface IUserRepository : IRepository<User>
    {
        bool FindOne(string username, string password);
    }
}
