namespace csharpMusicFestival.domain
{
    public class Ticket
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

        public override string ToString() => "{ShowId = " + ShowId + ", PurchaserName = " + PurchaserName + ", Number = " + Number + "}";
    }
}