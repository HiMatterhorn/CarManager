using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AmiFlota.Models.ViewModels
{
    public class ActiveBookingVM
    {
        public BookingVM BookingViewModel { get; set; }
        public List<TripVM> TripsHistory { get; set; }

    }
}
