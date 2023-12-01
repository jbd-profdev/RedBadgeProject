using RedBadgeProject.Shared.Models.Staff;

namespace RedBadgeProject.Services.Staff
{
    public interface IStaffService
    {
        void SetUserId(string userId);
        Task<bool> CreateStaffAsync(StaffCreate request);
        Task<List<StaffDetail>> GetAllStaffAsync();
        Task<StaffDetail?> GetStaffByIdAsync(int staffId);
        Task<bool> UpdateStaffAsync(StaffEdit request);
        Task<bool> DeleteStaffAsync(int staffId);
    }
}