using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class CalendarVM
    {
        public List<CarModel> Cars { get; set; }
        public BookingVM Booking { get; set; }
        public List<TripVM> TripsHistory { get; set; }

    }
}
