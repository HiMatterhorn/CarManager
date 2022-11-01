using Microsoft.AspNetCore.Http;
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
        [DisplayName("Seats")]
        public ushort SeatsNumber { get; set; }
        public ushort Doors { get; set; }
        public TrunkType Trunk { get; set; }
        [DisplayName("Horse Power")]
        public ushort HorsePower { get; set; }
        public ushort Engine { get; set; }

        [DisplayName("Tyres")]
        [MaxLength(10)]
        public string TyreSize { get; set; }

        public Fuel Fuel { get; set; }
        [DisplayName("Card PIN")]
        public ushort CardPin { get; set; }

        public string PhotoPath { get; set; }
        public virtual ICollection<BookingModel> Bookings { get; set; }
    }
}
