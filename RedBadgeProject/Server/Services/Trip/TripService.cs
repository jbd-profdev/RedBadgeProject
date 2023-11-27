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
                EndDate = request.EndDate
            };

            _dbcontext.Trips.Add(entity);
            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            if (numberOfChanges == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTripAsync(TripDelete tripId)
        {
            var tripEntity = await _dbcontext.Trips.FindAsync(tripId.Id);

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
                LocationToId = entity.LocationToId,
                VehicleId = entity.VehicleId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Capacity = entity.Capacity
            }).ToListAsync();

            return trips;
        }

        public Task<TripDetail?> GetTripByIdAsync(int tripId)
        {
            throw new NotImplementedException();
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