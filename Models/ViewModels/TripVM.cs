using AmiFlota.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AmiFlota.Models.ViewModels
{
    public class TripVM
    {
        public int Id { get; set; }
        [Required]
        public int BookingId { get; set; }
        [DisplayName("Active")]
        public bool Active { get; set; }

        [Required]
        public uint StartKm { get; set; }

        [Required]
        [DisplayName("Start Location")]
        public string StartLocation { get; set; }

        public uint EndKm { get; set; }

        [DisplayName("End Location")]
        public string EndLocation { get; set; }

        public string Project { get; set; }

        public float Cost { get; set; }
        public string CostRemarks { get; set; }

        public string User { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}
