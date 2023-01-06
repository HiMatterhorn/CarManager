using AmiFlota.Dto;
using AmiFlota.Enums;
using AmiFlota.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public interface ITripService
    {
        public int StartTrip(TripVM bookingVM);
        public int CreateTrip(TripVM tripVM);
        public int UpdateTrip(TripVM tripVM);
        public int FinishTrip(TripVM tripVM);

        public List<TripVM> TripsHistoryByCarVin(string vin);
        public TripEndVM GetActiveTripByBookingId(int bookingId);

        public int ChangeBookingStatus(int bookingId, BookingStatus newBookingStatus);
        public List<TripVM> TripsHistoryByBookingId(int id);
        public Task<List<string>> GetAllStartLocations();
        public Task<List<string>> GetAllEndLocations();
        public uint HighestMileageValue(int bookingId);
    }
}
