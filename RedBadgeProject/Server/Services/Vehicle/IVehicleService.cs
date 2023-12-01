using RedBadgeProject.Shared.Models.Vehicle;

namespace RedBadgeProject.Services.Vehicle
{
    public interface IVehicleService
    {
        void SetUserId(string userId);
        Task<bool> CreateVehicleAsync(VehicleCreate request);
        Task<List<VehicleDetail>> GetAllVehiclesAsync();
        Task<VehicleDetail?> GetVehicleByIdAsync(int vehicleId);
        Task<bool> UpdateVehicleAsync(VehicleEdit request);
        Task<bool> DeleteVehicleAsync(int vehicleId);
    }
}