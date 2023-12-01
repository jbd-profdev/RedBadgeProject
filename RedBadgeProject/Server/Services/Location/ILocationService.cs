using RedBadgeProject.Shared.Models.Location;

namespace RedBadgeProject.Services.Location
{
    public interface ILocationService
    {
        void SetUserId(string userId);
        Task<bool> CreateLocationAsync(LocationCreate request);
        Task<List<LocationDetail>> GetAllLocationsAsync();
        Task<LocationDetail?> GetLocationByIdAsync(int locationId);
        Task<bool> UpdateLocationAsync(LocationEdit request);
        Task<bool> DeleteLocationAsync(int locationId);
    }
}