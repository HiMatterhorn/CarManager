using System;
using System.ComponentModel;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class TripVM
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        [DisplayName("Active")]
        public bool Active { get; set; }
        public uint StartKm { get; set; }

        [DisplayName("Start Location")]
        public string StartLocation { get; set; }

        /*        public DateTime StartTimestamp { get; set; }*/

        public uint EndKm { get; set; }

        [DisplayName("End Location")]
        public string EndLocation { get; set; }

        /*        public DateTime EndTimestamp { get; set; }*/

        public string Project { get; set; }

        public float Cost { get; set; }
        public string CostRemarks { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}
