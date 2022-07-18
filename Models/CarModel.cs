using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models
{
    [Table("Cars")]
    public class CarModel
    {
        [Key]
        [Required]
        public string VIN { get; set; }

        [DisplayName("Registration Number")]
        public string RegistrationNumber { get; set; }
        [MaxLength(100)]
        public string Brand { get; set; }
        [MaxLength(100)]
        public string Model { get; set; }

        public int SeatsNumber { get; set; }

        public TrunkType Trunk { get; set; }

        public string PhotoPath { get; set; }

        public virtual ICollection<BookingModel> Bookings { get; set; }
    }
}
