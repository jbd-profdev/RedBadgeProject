using RedBadgeProject.Shared.Models.Trip;

namespace RedBadgeProject.Services.Trip
{
    public interface ITripService
    {
        void SetUserId(string userId);
        Task<bool> CreateTripAsync(TripCreate request);
        Task<List<TripDetail>> GetAllTripsAsync();
        Task<TripDetail?> GetTripByIdAsync(int tripId);
        Task<bool> UpdateTripAsync(TripEdit request);
        Task<bool> DeleteTripAsync(TripDelete tripId);
    }
}