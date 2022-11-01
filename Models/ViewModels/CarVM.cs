using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class CarVM
    {
        [Required]
        public string VIN { get; set; }
        public string RegistrationNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public int SeatsNumber { get; set; }
        public TrunkType Trunk { get; set; }
        public IFormFile PhotoPath { get; set; }
    }
}
