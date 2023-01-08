using AmiFlota.Dto;
using AmiFlota.Enums;
using AmiFlota.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmiFlota.Contracts
{
    public interface ITripService
    {
        public int StartTrip(TripStartVM bookingVM);
        public int CreateTrip(TripStartVM tripStartVM);
        public int UpdateTrip(TripEndVM tripEndVM);
        public int FinishTrip(TripEndVM tripEndVM);

        public List<TripVM> TripsHistoryByCarVin(string vin);
        public TripEndVM GetActiveTripByBookingId(int bookingId);

        public int ChangeBookingStatus(int bookingId, BookingStatus newBookingStatus);
        public List<TripVM> TripsHistoryByBookingId(int id);
        public Task<List<string>> GetAllStartLocations();
        public Task<List<string>> GetAllEndLocations();
        public uint HighestMileageValue(int bookingId);
    }
}
