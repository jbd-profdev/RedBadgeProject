namespace RedBadgeProject.Shared.Models.Company
{
    public class CompanyDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}