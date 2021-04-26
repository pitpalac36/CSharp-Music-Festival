using Model.domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.domain
{
    [Serializable]
    public class Artist : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        [Required]
        [StringLength(50)]
        public string Date { get; set; }

        [Required]
        public int AvailableTicketsNumber { get; set; }

        private Artist() { }

        public Artist(string name, string location, string date, int availableTicketsNumber)
        {
            Name = name;
            Location = location;
            Date = date;
            AvailableTicketsNumber = availableTicketsNumber;
        }
    }
}