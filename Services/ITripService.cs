using AmiFlota.Models.ViewModels;
using System.Threading.Tasks;

namespace AmiFlota.Services
{
    public interface ITripService
    {
        public Task<int> CreateInspection(TripVM bookingVM);
    }
}
