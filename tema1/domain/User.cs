
namespace csharpMusicFestival.domain
{
    class User
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public override string ToString()
        {
            return "Name : " + Name + ", Password : " + Password;
        }
    }
}
