using Microsoft.EntityFrameworkCore;
using RedBadgeProject.Server.Data;
using RedBadgeProject.Server.Models;
using RedBadgeProject.Shared.Models.Staff;

namespace RedBadgeProject.Services.Staff
{
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _dbcontext;
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        public StaffService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<bool> CreateStaffAsync(StaffCreate request)
        {
            StaffEntity entity = new()
            {
                Name = request.Name,
                CompanyId = request.CompanyId,
                CurrentLocationId = request.CurrentLocationId,
                RoleId = request.RoleId
            };

            _dbcontext.Staff.Add(entity);
            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            if (numberOfChanges == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteStaffAsync(StaffDelete staffId)
        {
            var staffEntity = await _dbcontext.Staff.FindAsync(staffId.Id);

            if (staffEntity is null)
                return false;

            _dbcontext.Staff.Remove(staffEntity);

            return await _dbcontext.SaveChangesAsync() == 1;
        }

        public async Task<List<StaffDetail>> GetAllStaffAsync()
        {
            List<StaffDetail> staff = await _dbcontext.Staff.Select(entity => new StaffDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                CompanyId = entity.CompanyId,
                CurrentLocationId = entity.CurrentLocationId,
                RoleId = entity.RoleId
            }).ToListAsync();

            return staff;
        }

        public async Task<StaffDetail?> GetStaffByIdAsync(int staffId)
        {
            StaffEntity? entity = await _dbcontext.Staff.FindAsync(staffId);
            return entity is null ? null : new StaffDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                CompanyId =entity.CompanyId,
                CurrentLocationId = entity.CurrentLocationId,
                RoleId = entity.RoleId
            };
        }

        public async Task<bool> UpdateStaffAsync(StaffEdit request)
        {
            StaffEntity? entity = await _dbcontext.Staff.FindAsync(request.Id);

            if (entity is null)
                return false;

            entity.Name = request.Name;
            entity.CompanyId = request.CompanyId;
            entity.CurrentLocationId = request.CurrentLocationId;
            entity.RoleId = request.RoleId;

            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}