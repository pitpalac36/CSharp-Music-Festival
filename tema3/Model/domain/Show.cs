using System;

namespace csharpMusicFestival.domain
{
    public class Show
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public int AvailableTicketsNumber { get; set; }
        public int SoldTicketsNumber { get; set; }

        public Show(int id, string artistName, string date, string location, int availableTicketsNumber, int soldTicketsNumber)
        {
            Id = id;
            ArtistName = artistName;
            Date = date;
            Location = location;
            AvailableTicketsNumber = availableTicketsNumber;
            SoldTicketsNumber = soldTicketsNumber;
        }

        public override string ToString()
        {
            return "Show{" +
                "id=" + Id +
                ", artistName='" + ArtistName + '\'' +
                ", date=" + Date +
                ", location='" + Location + '\'' +
                ", availableTicketsNumber=" + AvailableTicketsNumber +
                ", soldTicketsNumber=" + SoldTicketsNumber +
                '}';
        }

        public bool Equals(Show other)
        {
            if (this == other) return true;
            if (other == null) return false;
            return Id == other.Id &&
                    AvailableTicketsNumber == other.AvailableTicketsNumber &&
                    SoldTicketsNumber == other.SoldTicketsNumber &&
                    Equals(ArtistName, other.ArtistName) &&
                    Equals(Date, other.Date) &&
                    Equals(Location, other.Location);
        }
    }
}