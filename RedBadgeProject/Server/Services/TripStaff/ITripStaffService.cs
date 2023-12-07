using RedBadgeProject.Shared.Models.TripStaff;

namespace RedBadgeProject.Server.Services.TripStaff
{
    public interface ITripStaffService
    {
        void SetUserId(string userId);
        Task<bool> CreateTripStaffAsync(TripStaffCreate request);
        Task<TripStaffDetail?> GetTripStaffByIdAsync(int id);
        Task<List<TripStaffDetail>> GetAllTripStaffByTripIdAsync(int tripId);
        Task<List<TripStaffDetail>> GetAllTripStaffByStaffIdAsync(int staffId);
        Task<bool> UpdateTripStaffAsync(TripStaffEdit request);
        Task<bool> DeleteTripStaffAsync(int id);
    }
}