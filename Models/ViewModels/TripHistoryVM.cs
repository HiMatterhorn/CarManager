using System;
using System.ComponentModel;
using static AmiFlota.Utilities.Enums;
using System.Collections.Generic;

namespace AmiFlota.Models.ViewModels
{
    public class TripHistoryVM
    {
        public List<TripVM> Data { get; set; }
        public string User { get; set; }
    }
}
