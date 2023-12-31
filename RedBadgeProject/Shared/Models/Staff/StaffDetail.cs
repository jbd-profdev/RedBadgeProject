namespace RedBadgeProject.Shared.Models.Staff
{
    public class StaffDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string Company { get; set; } = string.Empty;
        public int CurrentLocationId { get; set; }
        public string Location { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}