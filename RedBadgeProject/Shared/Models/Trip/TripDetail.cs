// using RedBadgeProject.Server.Models;

namespace RedBadgeProject.Shared.Models.Trip
{
    public class TripDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LocationFromId { get; set; }
        public int LocationToId { get; set; }
        public int VehicleId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int Capacity { get; set; }
        // public virtual ICollection<StaffEntity> StaffList { get; set; } = new List<StaffEntity>();
    }
}