﻿using AmiFlota.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmiFlota.Models
{
    public class BookingModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Column("User Id")]
        public string UserId { get; set; }
        [Required]
        public string CarVIN { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public string Description { get; set; }
        [Required]
        public string ProjectCost { get; set; }

        public DateTime CarTakenUTC { get; set; }

        public DateTime CarReturnedUTC { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public virtual ApplicationUserModel ApplicationUserModels { get; set; }
        public virtual CarModel CarModels { get; set; }

        public virtual List<TripModel> Trips { get; set; }

    }
}
