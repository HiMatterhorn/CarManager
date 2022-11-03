using AmiFlota.Models.ViewModels;
using System.Collections.Generic;
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

        public List<TripHistoryVM> TripsHistoryByCarVin(string vin);
        public List<CalendarVM> TripsByCarVinList(List<string> selectedCars);
        public TripVM GetActiveTripByBookingId(int bookingId);

        public int ChangeBookingStatus(int bookingId, BookingStatus newBookingStatus);


    }
}
