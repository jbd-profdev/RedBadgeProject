// using RedBadgeProject.Server.Models;

namespace RedBadgeProject.Shared.Models.Staff
{
    public class StaffDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public int CurrentLocationId { get; set; }
        public int RoleId { get; set; }
        // public virtual ICollection<TripEntity> TripList { get; set; } = new List<TripEntity>();
    }
}