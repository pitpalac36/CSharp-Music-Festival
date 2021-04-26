using Model.domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.domain
{
    [Serializable]
    public class Show : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ArtistName { get; set; }

        [Required]
        [StringLength(50)]
        public string Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        [Required]
        public int AvailableTicketsNumber { get; set; }

        [Required]
        public int SoldTicketsNumber { get; set; }

        private Show() { }

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