using csharpMusicFestival.domain;
using System.Data.SQLite;
using System.Collections.Generic;
using log4net;

namespace csharpMusicFestival.repository
{
    public class TicketDbRepository : ITicketRepository
    {
        private static readonly ILog logger = LogManager.GetLogger("TicketDbRepository");

        public List<Ticket> FindAll()
        {
            logger.Info("FindAll entry");
            List<Ticket> tickets = new List<Ticket>();
            string query = "SELECT * FROM tickets";
            var connection = DbUtils.getConnection();
            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        tickets.Add(new Ticket(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    logger.InfoFormat("{0} tickets found; exiting FindAll...", tickets.Count);
                }
            }
            DbUtils.closeConnection();
            return tickets;
        }

        public void Save(Ticket ticket)
        {
            logger.Info("Save entry");
            var insert = "INSERT INTO tickets (showId, purchaserName, number) values (@showId, @purchaserName, @number)";

            var connection = DbUtils.getConnection();
            using (var command = new SQLiteCommand(insert, connection))
            {
                var param1 = command.CreateParameter();
                param1.ParameterName = "@showId";
                param1.Value = ticket.ShowId;

                var param2 = command.CreateParameter();
                param2.ParameterName = "@purchaserName";
                param2.Value = ticket.PurchaserName;

                var param3 = command.CreateParameter();
                param3.ParameterName = "@number";
                param3.Value = ticket.Number;

                command.Parameters.Add(param1);
                command.Parameters.Add(param2);
                command.Parameters.Add(param3);

                var affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 1)
                    logger.Info("insert successfull!");
                else
                    logger.ErrorFormat("{0} rows affected...", affectedRows);
            }
            DbUtils.closeConnection();
        }
    }
}
