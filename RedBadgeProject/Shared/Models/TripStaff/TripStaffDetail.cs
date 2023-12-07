namespace RedBadgeProject.Shared.Models.TripStaff
{
    public class TripStaffDetail
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string Staff { get; set; } = string.Empty;
        public int TripId { get; set; }
        public string Trip { get; set; } = string.Empty;
    }
}