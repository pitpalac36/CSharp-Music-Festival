using System;

namespace csharpMusicFestival.domain
{
    class Show
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int AvailableTicketsNumber { get; set; }
        public int SoldTicketsNumber { get; set; }

        public Show (int id, string artistName, DateTime date, string location, int availableTicketsNumber, int soldTicketsNumber)
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
    }
}
