using csharpMusicFestival.domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace csharpMusicFestival.repository
{
    class ShowDbRepository : IShowRepository
    {
        private static readonly ILog logger = LogManager.GetLogger("ShowDbRepository");

        public List<Show> FindAll()
        {
            logger.Info("FindAll entry");
            List<Show> shows = new List<Show>();
            string query = "SELECT * FROM shows";
            var connection = DbUtils.getConnection();
            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        shows.Add(new Show(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5)));
                    logger.InfoFormat("{0} shows found; exiting FindAll...", shows.Count);
                }
            }
            DbUtils.closeConnection();
            return shows;
        }

        public List<Show> FindArtists(DateTime date)
        {
            logger.Info("FindAll entry");
            List<Show> shows = new List<Show>();
            string query = "SELECT * FROM shows WHERE date=@date";
            var connection = DbUtils.getConnection();
            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var param1 = command.CreateParameter();
                    param1.ParameterName = "@date";
                    param1.Value = date;

                    command.Parameters.Add(param1);

                    while (reader.Read())
                        shows.Add(new Show(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5)));
                    logger.InfoFormat("{0} shows found by date; exiting FindAll...", shows.Count);
                }
            }
            DbUtils.closeConnection();
            return shows;
        }

        public Show FindOne(int id)
        {
            logger.Info("FindOne entry");
            string query = "SELECT * FROM shows WHERE id=@id";
            var connection = DbUtils.getConnection();
            Show show = null;
            using (var command = new SQLiteCommand(query, connection))
            {
                var param1 = command.CreateParameter();
                param1.ParameterName = "@id";
                param1.Value = id;

                command.Parameters.Add(param1);

                using (var reader = command.ExecuteReader())
                {
                    logger.InfoFormat("findOne id {0} - {1}", id, reader.HasRows);
                    if (reader.Read())
                    {
                        show = new Show(reader.GetInt32(0), reader.GetString(1), DateTime.Parse(reader.GetString(2)), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
                    }
                    DbUtils.closeConnection();
                    return show;
                }
            }
        }

        public void Update(Show show)
        {
            logger.Info("Update entry");
            var update = "UPDATE shows SET artistName=@artistName, dateOfEvent=@dateOfEvent, location=@location, availableTicketsNo=@availableTicketsNo, soldTicketsNo=@soldTicketsNo WHERE id=@id";

            var connection = DbUtils.getConnection();
            using (var command = new SQLiteCommand(update, connection))
            {
                var param1 = command.CreateParameter();
                param1.ParameterName = "@artistName";
                param1.Value = show.ArtistName;

                var param2 = command.CreateParameter();
                param2.ParameterName = "@dateOfEvent";
                param2.Value = show.Date;

                var param3 = command.CreateParameter();
                param3.ParameterName = "@location";
                param3.Value = show.Location;

                var param4 = command.CreateParameter();
                param4.ParameterName = "@availableTicketsNo";
                param4.Value = show.AvailableTicketsNumber;

                var param5 = command.CreateParameter();
                param5.ParameterName = "@soldTicketsNo";
                param5.Value = show.SoldTicketsNumber;

                command.Parameters.Add(param1);
                command.Parameters.Add(param2);
                command.Parameters.Add(param3);
                command.Parameters.Add(param4);
                command.Parameters.Add(param5);

                var affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 1)
                    logger.Info("update successfull!");
                else
                    logger.ErrorFormat("{0} rows affected...", affectedRows);
            }
            DbUtils.closeConnection();
        }
    }
}
