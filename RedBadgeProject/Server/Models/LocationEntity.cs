using System.ComponentModel.DataAnnotations;

namespace RedBadgeProject.Server.Models
{
    public class LocationEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}