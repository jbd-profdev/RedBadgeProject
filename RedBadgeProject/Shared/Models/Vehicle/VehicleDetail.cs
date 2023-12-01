namespace RedBadgeProject.Shared.Models.Vehicle
{
    public class VehicleDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string Company { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}