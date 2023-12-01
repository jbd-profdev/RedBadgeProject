using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedBadgeProject.Server.Models
{
    public class CompanyEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }
        public virtual LocationEntity Location { get; set; }
    }
}