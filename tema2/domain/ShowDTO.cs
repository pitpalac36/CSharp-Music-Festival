

namespace csharpMusicFestival.domain
{
    public class ShowDTO
    {
        public string Name { get; set;}
        public string Location { get; set; }
        public string Date { get; set; }

        public int AvailableTicketsNumber { get; set; }

        public ShowDTO(string name, string location, string date, int availableTicketsNumber)
        {
            Name = name;
            Location = location;
            Date = date;
            AvailableTicketsNumber = availableTicketsNumber;
        }
    }
}
