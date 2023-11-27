namespace RedBadgeProject.Shared.Models.Company
{
    public class CompanyCreate
    {
        public string Name { get; set; } = string.Empty;
        public int LocationId { get; set; }
    }
}