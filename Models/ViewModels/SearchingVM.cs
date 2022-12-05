using System;
using System.ComponentModel.DataAnnotations;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class SearchingVM
    {
        public string RegistrationNumber { get; set; }

        public string Brand { get; set; }
        [MaxLength(100)]
        public string Model { get; set; }

        public int SeatsNumber { get; set; }

        public TrunkType Trunk { get; set; }

        public string PhotoPath { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
