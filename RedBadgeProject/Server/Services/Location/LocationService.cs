using Microsoft.EntityFrameworkCore;
using RedBadgeProject.Server.Data;
using RedBadgeProject.Server.Models;
using RedBadgeProject.Shared.Models.Location;

namespace RedBadgeProject.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _dbcontext;
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        public LocationService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<bool> CreateLocationAsync(LocationCreate request)
        {
            LocationEntity entity = new()
            {
                Name = request.Name
            };

            _dbcontext.Locations.Add(entity);
            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            if (numberOfChanges == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLocationAsync(LocationDelete locationId)
        {
            var locationEntity = await _dbcontext.Locations.FindAsync(locationId.Id);

            if (locationEntity is null)
                return false;

            _dbcontext.Locations.Remove(locationEntity);

            return await _dbcontext.SaveChangesAsync() == 1;
        }

        public async Task<List<LocationDetail>> GetAllLocationsAsync()
        {
            List<LocationDetail> locations = await _dbcontext.Locations.Select(entity => new LocationDetail
            {
                Id = entity.Id,
                Name = entity.Name
            }).ToListAsync();

            return locations;
        }

        public async Task<LocationDetail?> GetLocationByIdAsync(int locationId)
        {
            LocationEntity? entity = await _dbcontext.Locations.FindAsync(locationId);
            return entity is null ? null : new LocationDetail
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<bool> UpdateLocationAsync(LocationEdit request)
        {
            LocationEntity? entity = await _dbcontext.Locations.FindAsync(request.Id);

            if (entity is null)
                return false;

            entity.Name = request.Name;

            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}