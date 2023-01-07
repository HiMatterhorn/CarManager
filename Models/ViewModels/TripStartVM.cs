using AmiFlota.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AmiFlota.Models.ViewModels
{
    public class TripStartVM
    {
        public int Id { get; set; }
        [Required]
        public int BookingId { get; set; }
        [DisplayName("Active")]
        public bool Active { get; set; }

/*        [Required]
        [Remote(action: "isStartOdoValid", controller: "Trip", AdditionalFields = "BookingId")]*/
        public uint StartKm { get; set; }

        [Required]
        [DisplayName("Start Location")]
        
        public string StartLocation { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}
