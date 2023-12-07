using Microsoft.EntityFrameworkCore;
using RedBadgeProject.Server.Data;
using RedBadgeProject.Server.Models;
using RedBadgeProject.Shared.Models.TripStaff;

namespace RedBadgeProject.Server.Services.TripStaff
{
    public class TripStaffService : ITripStaffService
    {
        private readonly ApplicationDbContext _dbcontext;
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        public TripStaffService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<bool> CreateTripStaffAsync(TripStaffCreate request)
        {
            TripStaffEntity entity = new()
            {
                Id = request.Id,
                StaffId = request.StaffId,
                TripId = request.TripId
            };

            _dbcontext.TripStaff.Add(entity);
            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            if (numberOfChanges == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<TripStaffDetail?> GetTripStaffByIdAsync(int id)
        {
            TripStaffEntity? entity = await _dbcontext.TripStaff
            .Include(i => i.Trip)
            .Include(i => i.Staff)
            .FirstOrDefaultAsync(i => i.Id == id);
            return entity is null ? null : new TripStaffDetail
            {
                Id = entity.Id,
                TripId = entity.TripId,
                Trip = entity.Trip.Name,
                StaffId = entity.StaffId,
                Staff = entity.Staff.Name
            };
        }

        public async Task<List<TripStaffDetail>> GetAllTripStaffByTripIdAsync(int tripId)
        {
            List<TripStaffDetail> group = await _dbcontext.TripStaff
            .Where(i => i.TripId == tripId)
            .Include(i => i.Trip)
            .Include(i => i.Staff)
            .Select(entity => new TripStaffDetail
            {
                Id = entity.Id,
                TripId = entity.TripId,
                Trip = entity.Trip.Name,
                Staff = entity.Staff.Name,
                StaffId = entity.StaffId
            }).ToListAsync();

            return group;
        }

        public async Task<List<TripStaffDetail>> GetAllTripStaffByStaffIdAsync(int staffId)
        {
            List<TripStaffDetail> group = await _dbcontext.TripStaff
            .Where(i => i.StaffId == staffId)
            .Include(i => i.Trip)
            .Include(i => i.Staff)
            .Select(entity => new TripStaffDetail
            {
                Id = entity.Id,
                TripId = entity.TripId,
                Trip = entity.Trip.Name,
                Staff = entity.Staff.Name,
                StaffId = entity.StaffId
            }).ToListAsync();

            return group;
        }

        public async Task<bool> UpdateTripStaffAsync(TripStaffEdit request)
        {
            TripStaffEntity? entity = await _dbcontext.TripStaff.FindAsync(request.Id);

            if (entity is null)
                return false;

            entity.TripId = request.TripId;
            entity.StaffId = request.StaffId;

            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTripStaffAsync(int id)
        {
            var entity = await _dbcontext.TripStaff.FindAsync(id);

            if (entity is null)
                return false;

            _dbcontext.TripStaff.Remove(entity);

            return await _dbcontext.SaveChangesAsync() == 1;
        }

    }
}