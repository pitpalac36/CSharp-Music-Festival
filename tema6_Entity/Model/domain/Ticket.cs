using Model.domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.domain
{
    [Serializable]
    public class Ticket : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Show")]
        public int ShowId { get; set; }

        [Required]
        [StringLength(50)]
        public string PurchaserName { get; set; }

        [Required]
        public int Number { get; set; }

        private Ticket() { }

        public Ticket(int showId, string purchaserName, int number)
        {
            ShowId = showId;
            PurchaserName = purchaserName;
            Number = number;
        }

        public override string ToString() => "{ShowId = " + ShowId + ", PurchaserName = " + PurchaserName + ", Number = " + Number + "}";
    }
}