using AmiFlota.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AmiFlota.Models.ViewModels
{
    public class TripEndVM
    {
        public int Id { get; set; }
        [Required]
        public int BookingId { get; set; }
        [DisplayName("Active")]
        public bool Active { get; set; }

/*        [Remote(action: "isEndOdoValid", controller: "Trip", AdditionalFields = "BookingId")]*/
        public uint EndKm { get; set; }

        [DisplayName("End Location")]
        public string EndLocation { get; set; }

        [Required]
        public string Project { get; set; }

        public float Cost { get; set; }
        public string CostRemarks { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}
