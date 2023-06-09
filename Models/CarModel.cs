﻿using AmiFlota.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [MaxLength(12)]
        public string Engine { get; set; }

        [DisplayName("Tyres")]
        [MaxLength(10)]
        public string TyreSize { get; set; }

        public Fuel Fuel { get; set; }
        [DisplayName("Card PIN")]
        public string CardPin { get; set; }

        public DateTime Insurance { get; set; }

        public DateTime TechnicalReview { get; set; }

        public string PhotoPath { get; set; }
        public virtual ICollection<BookingModel> Bookings { get; set; }
    }
}
