using csharpMusicFestival.domain;
using log4net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace csharpMusicFestival.repository
{
    
    class UserDbRepository : IUserRepository
    {
        private static readonly ILog logger = LogManager.GetLogger("UserDbRepository");

        public UserDbRepository()
        {
            logger.Info("UserDbRepository constructor");
        }

        public bool FindOne(string username, string password)
        {
            logger.Info("FindOne entry");
            string query = "SELECT 1 FROM users WHERE username=@username AND password=@password";
            var connection = DbUtils.getConnection();
            using (var command = new SQLiteCommand(query, connection))
            {
                var param1 = command.CreateParameter();
                param1.ParameterName = "@username";
                param1.Value = username;

                var param2 = command.CreateParameter();
                param2.ParameterName = "@password";
                param2.Value = password;

                command.Parameters.Add(param1);
                command.Parameters.Add(param2);

                using (var reader = command.ExecuteReader())
                {
                    logger.InfoFormat("findOne - {0} and {1} are {2}", username, password, reader.HasRows);

                    bool wasFound = reader.Read();
                    DbUtils.closeConnection();
                    return wasFound;
                }
            }
        }

        public List<User> FindAll()
        {
            logger.Info("FindAll entry");
            List<User> users = new List<User>();
            string query = "SELECT * FROM users";
            var connection = DbUtils.getConnection();
            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        users.Add(new User(reader.GetString(0), reader.GetString(1)));
                    logger.InfoFormat("{0} users found; exiting FindAll...", users.Count);
                }
            }
            DbUtils.closeConnection();
            return users;

        }

        /*
        [TestFixture]
        public class UserRepoTest
        {

            IUserRepository repo = new UserDbRepository();

            [Test]
            public void TestExists()
            {
                Assert.IsTrue(repo.FindOne("test1", "test1"));
                Assert.IsFalse(repo.FindOne("test0", "test0"));
                int i = 1;
                foreach (var user in repo.FindAll())
                {
                    Assert.IsTrue(user.Name.Contains("test" + i));
                    Assert.IsTrue(user.Password.Contains("test" + i));
                    Assert.AreEqual(user.Name, user.Password);
                    i++;
                }
            }
        }
        */

    }
}
