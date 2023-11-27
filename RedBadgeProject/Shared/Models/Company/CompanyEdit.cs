namespace RedBadgeProject.Shared.Models.Company
{
    public class CompanyEdit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LocationId { get; set; }
    }
}