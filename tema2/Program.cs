using csharpMusicFestival.domain;
using csharpMusicFestival.repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace csharpMusicFestival
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new LoginForm());

            //TestUserRepo();
            //TestShowRepo();
        }

        static void TestUserRepo()
        {
            IUserRepository repo = new UserDbRepository();
            Assert.IsTrue(repo.FindOne("test1", "test1"));
            Assert.IsFalse(repo.FindOne("test0", "test0"));
            int i = 1;
            foreach (var user in repo.FindAll())
            {
                Console.WriteLine(user.ToString());
                Assert.IsTrue(user.Name.Contains("test" + i));
                Assert.IsTrue(user.Password.Contains("test" + i));
                Assert.AreEqual(user.Name, user.Password);
                i++;
            }
        }


        static void TestShowRepo()
        {
            IShowRepository repo = new ShowDbRepository();
            Show show = repo.FindOne(1);
            Assert.AreEqual("descend into despair", show.ArtistName);
            Assert.AreEqual("Cluj-Napoca, str. Grivitei, 2/30", show.Location);
            List<Show> shows = repo.FindAll();
            Assert.IsTrue(shows[0].Equals(show));
            Console.WriteLine(show);
            foreach(var each in shows)
                Console.WriteLine(each);
        }
    }
}
