using AmiFlota.Models.ViewModels;
using System.Threading.Tasks;
using static AmiFlota.Utilities.Enums;

namespace AmiFlota.Services
{
    public interface ITripService
    {
        public int StartTrip(TripVM bookingVM);
        public int CreateTrip(TripVM tripVM);
        public int UpdateTrip(TripVM tripVM);
        public int FinishTrip(TripVM tripVM);
        public TripVM GetTripByBookingId(int bookingId);

        public int ChangeBookingStatus(int bookingId, BookingStatus newBookingStatus);
    }
}
