using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class ActiveBookingVM
    {

        public BookingVM BookingViewModel { get; set; }
        public List<TripVM> TripsHistory { get; set; }

    }
}
