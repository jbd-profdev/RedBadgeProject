namespace RedBadgeProject.Shared.Models.Vehicle
{
    public class VehicleEdit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public int Capacity { get; set; }
    }
}