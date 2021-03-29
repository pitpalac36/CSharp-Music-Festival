
namespace csharpMusicFestival.domain
{
    class Ticket
    {
        public int ShowId { get; set; }
        public string PurchaserName { get; set; }
        public int Number { get; set; }

        public Ticket(int showId, string purchaserName, int number)
        {
            ShowId = showId;
            PurchaserName = purchaserName;
            Number = number;
        }
    }
}
