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

        [Column("Active")]
        public bool Active { get; set; }

        [Required]
        [Column("Start Mileage")]
        public uint StartKm { get; set; }

        /*        [Column("Start Inspection")]
                [ForeignKey("InspectionModels")]
                public InspectionModel StartInspectionId { get; set; }*/
        
        [Column("Start Location")]
        public string StartLocation { get; set; }

        [Column("Start UTC Time")]
        public DateTime StartTimestampUTC { get; set; }

        [Required]
        [Column("End Mileage")]
        public uint EndKm { get; set; }

        [Column("End Location")]
        public string EndLocation { get; set; }

        /*        [Column("End Inspection")]
                [ForeignKey("InspectionModels")]
                public InspectionModel EndInspectionId { get; set; }*/

        [Column("End UTC Time")]
        public DateTime EndTimestampUTC { get; set; }

        public string Project { get; set; }
        public float Cost { get; set; }
        [Column("Costs remarks")]
        public string CostRemarks { get; set; }

        [Required]
        [Column("Booking ID")]
        [ForeignKey("BookingModels")]
        public int BookingRefId { get; set; }

        public virtual BookingModel BookingModels { get; set; }
        /*        public virtual InspectionModel InspectionModels { get; set; }*/

    }
}
