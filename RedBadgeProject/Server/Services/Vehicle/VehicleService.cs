using Microsoft.EntityFrameworkCore;
using RedBadgeProject.Server.Data;
using RedBadgeProject.Server.Models;
using RedBadgeProject.Shared.Models.Vehicle;

namespace RedBadgeProject.Services.Vehicle
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _dbcontext;
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        public VehicleService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<bool> CreateVehicleAsync(VehicleCreate request)
        {
            VehicleEntity entity = new()
            {
                Name = request.Name,
                CompanyId = request.CompanyId,
                Capacity = request.Capacity
            };

            _dbcontext.Vehicles.Add(entity);
            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            if (numberOfChanges == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteVehicleAsync(VehicleDelete vehicleId)
        {
            var vehicleEntity = await _dbcontext.Vehicles.FindAsync(vehicleId.Id);

            if (vehicleEntity is null)
                return false;

            _dbcontext.Vehicles.Remove(vehicleEntity);

            return await _dbcontext.SaveChangesAsync() == 1;
        }

        public async Task<List<VehicleDetail>> GetAllVehiclesAsync()
        {
            List<VehicleDetail> vehicles = await _dbcontext.Vehicles.Select(entity => new VehicleDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                CompanyId = entity.CompanyId,
                Capacity = entity.Capacity
            }).ToListAsync();

            return vehicles;
        }

        public async Task<VehicleDetail?> GetVehicleByIdAsync(int vehicleId)
        {
            VehicleEntity? entity = await _dbcontext.Vehicles.FindAsync(vehicleId);
            return entity is null ? null : new VehicleDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                CompanyId = entity.CompanyId,
                Capacity = entity.Capacity
            };
        }

        public async Task<bool> UpdateVehicleAsync(VehicleEdit request)
        {
            VehicleEntity? entity = await _dbcontext.Vehicles.FindAsync(request.Id);

            if (entity is null)
                return false;

            entity.Name = request.Name;
            entity.CompanyId = request.CompanyId;
            entity.Capacity = request.Capacity;

            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}