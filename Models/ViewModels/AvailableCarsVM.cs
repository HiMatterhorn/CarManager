using System;
using System.Collections.Generic;

namespace AmiFlota.Models.ViewModels
{
    public class AvailableCarsVM
    {
        public List<CarModel> AvailableCars {get; set;}

        public DateTime StartDate {get; set;}

        public DateTime EndDate {get; set;}
    }
}
