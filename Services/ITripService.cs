using AmiFlota.Models.ViewModels;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public interface ITripService
    {
        public void CreateTrip(TripVM bookingVM);
        public void UpdateTrip(TripVM tripVM);
        public TripVM GetTripByBookingId(int bookingId);

    }
}
