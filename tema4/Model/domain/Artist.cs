using System;

namespace csharpMusicFestival.domain
{
    [Serializable]
    public class Artist
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }

        public int AvailableTicketsNumber { get; set; }

        public Artist(string name, string location, string date, int availableTicketsNumber)
        {
            Name = name;
            Location = location;
            Date = date;
            AvailableTicketsNumber = availableTicketsNumber;
        }
    }
}