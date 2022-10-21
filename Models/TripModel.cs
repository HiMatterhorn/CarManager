using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmiFlota.Models
{
    public class TripModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Column("Start Mileage")]
        public uint StartKm { get; set; }

        [Required]
        [Column("End Mileage")]
        public uint EndKm { get; set; }

        [Required]
        [Column("Booking ID")]
        [ForeignKey("BookingModels")]
        public int BookingRefId { get; set; }

        public float Costs { get; set; }
        [Column("Costs remarks")]
        public string CostsRemarks { get; set; }

        [Column("Inspection")]
        public bool CarInspectionDone { get; set;}
        [Column("Inspection remarks")]
        public string CarInsepctionRemarks { get; set; }

        public bool Damages { get; set; }
        [Column("Damages description")]
        public string DamagesDescription { get; set; }

        public bool Active { get; set; }

        public virtual BookingModel BookingModels { get; set; }

    }
}
