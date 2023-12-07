namespace RedBadgeProject.Shared.Models.TripStaff
{
    public class TripStaffCreate
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int TripId { get; set; }
    }
}