using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class CarDetailsVM
    {
        public CarModel Car { get; set; }

        public List<TripVM> TripsHistory { get; set; }
    }
}
