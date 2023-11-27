namespace RedBadgeProject.Shared.Models.Staff
{
    public class StaffEdit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public int CurrentLocationId { get; set; }
        public int RoleId { get; set; }
    }
}