using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class TripVM
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public uint StartKm { get; set; }

        /*        public InspectionModel StartInspectionId { get; set; }*/

        public uint EndKm { get; set; }

        /*        public InspectionModel EndInspectionId { get; set; }*/

        public float Cost { get; set; }
        public string CostRemarks { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}
