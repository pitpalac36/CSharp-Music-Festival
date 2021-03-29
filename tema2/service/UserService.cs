using csharpMusicFestival.repository;

namespace csharpMusicFestival.service
{
    public class UserService
    {
        private IUserRepository repo;

        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }

        public bool Login(string username, string password)
        {
            return repo.FindOne(username, password);
        }
    }
}
