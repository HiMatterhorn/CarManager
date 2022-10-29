﻿using System;
using System.ComponentModel.DataAnnotations;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class BookingVM
    {

        public int? Id { get; set; }
        public string UserName { get; set; }

        //FROM CarModel
        public string RegistrationNumber { get; set; }
        public string PhotoPath { get; set; }

        //FROM BookingModel
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Destination { get; set; }
        public string ProjectCost { get; set; }
        public BookingStatus BookingStatus { get; set; }

    }
}
