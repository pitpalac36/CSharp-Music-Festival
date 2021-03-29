using System.Data.SQLite;
using System;
using System.Configuration;
using log4net;

namespace csharpMusicFestival.repository
{
    public class DbUtils
    {
        private static SQLiteConnection instance = null;
        private static readonly ILog logger = LogManager.GetLogger("DbUtils");
        public static SQLiteConnection getConnection()
        {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed)
            {
                instance = createConnection();
                instance.Open();
                logger.Info("instance is open!");
            }
            return instance;
        }

        private static SQLiteConnection createConnection()
        {
            logger.Info("createConnection entry");
            string connectionString = "";
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["musicFestival"];
            if (settings != null)
            {
                connectionString = settings.ConnectionString;
                logger.InfoFormat("connectionString : {0}", connectionString);
            }
            return new SQLiteConnection(connectionString);
        }

        public static void closeConnection()
        {
            if (instance != null && instance.State != System.Data.ConnectionState.Closed)
            {
                instance.Close();
                logger.Info("connection successfully closed!");
            }
        }
    }
}
