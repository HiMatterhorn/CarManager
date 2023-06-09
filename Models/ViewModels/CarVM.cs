﻿using AmiFlota.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AmiFlota.Models.ViewModels
{
    public class CarVM
    {
        [Required]
        public string VIN { get; set; }
        [DisplayName("Registration Number")]
        public string RegistrationNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        [DisplayName("Seats")]
        public ushort SeatsNumber { get; set; }
        public ushort Doors { get; set; }
        public TrunkType Trunk { get; set; }
        [DisplayName("Horse Power")]
        public ushort HorsePower { get; set; }
        [MaxLength(12)]
        public string Engine { get; set; }
        [DisplayName("Tyres")]
        [MaxLength(10)]
        public string TyreSize { get; set; }
        [DisplayName("Fuel")]
        public Fuel Fuel { get; set; }
        [DisplayName("Card PIN")]
        public string CardPin { get; set; }

        public DateTime Insurance { get; set; }

        public DateTime TechnicalReview { get; set; }
        public IFormFile PhotoPath { get; set; }
    }
}
