using Microsoft.EntityFrameworkCore;
using RedBadgeProject.Server.Data;
using RedBadgeProject.Server.Models;
using RedBadgeProject.Shared.Models.Trip;

namespace RedBadgeProject.Services.Trip
{
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext _dbcontext;
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        public TripService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<bool> CreateTripAsync(TripCreate request)
        {
            TripEntity entity = new()
            {
                Name = request.Name,
                LocationFromId = request.LocationFromId,
                LocationToId = request.LocationToId,
                VehicleId = request.VehicleId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Capacity = request.Capacity
            };

            _dbcontext.Trips.Add(entity);
            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            if (numberOfChanges == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTripAsync(int tripId)
        {
            var tripEntity = await _dbcontext.Trips.FindAsync(tripId);

            if (tripEntity is null)
                return false;

            _dbcontext.Trips.Remove(tripEntity);

            return await _dbcontext.SaveChangesAsync() == 1;
        }

        public async Task<List<TripDetail>> GetAllTripsAsync()
        {
            List<TripDetail> trips = await _dbcontext.Trips.Select(entity => new TripDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                LocationFromId = entity.LocationFromId,
                LocationFrom = entity.LocationFrom.Name,
                LocationToId = entity.LocationToId,
                LocationTo = entity.LocationTo.Name,
                VehicleId = entity.VehicleId,
                Vehicle = entity.Vehicle.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Capacity = entity.Capacity
            }).ToListAsync();

            return trips;
        }

        public async Task<TripDetail?> GetTripByIdAsync(int tripId)
        {
            TripEntity? entity = await _dbcontext.Trips
            .Include(i => i.LocationFrom)
            .Include(i => i.LocationTo)
            .Include(i => i.Vehicle)
            .FirstOrDefaultAsync(i => i.Id == tripId);
            return entity is null ? null : new TripDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                LocationFromId = entity.LocationFromId,
                LocationFrom = entity.LocationFrom.Name,
                LocationToId = entity.LocationToId,
                LocationTo = entity.LocationTo.Name,
                VehicleId = entity.VehicleId,
                Vehicle = entity.Vehicle.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Capacity = entity.Capacity
            };
        }

        public async Task<bool> UpdateTripAsync(TripEdit request)
        {
            TripEntity? entity = await _dbcontext.Trips.FindAsync(request.Id);

            if (entity is null)
                return false;

            entity.Name = request.Name;
            entity.LocationFromId = request.LocationFromId;
            entity.LocationToId = request.LocationToId;
            entity.VehicleId = request.VehicleId;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;
            entity.Capacity = request.Capacity;

            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}