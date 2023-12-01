using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedBadgeProject.Server.Models
{
    public class VehicleEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
        public virtual CompanyEntity Company { get; set; }

        public int Capacity { get; set; }

    }
}