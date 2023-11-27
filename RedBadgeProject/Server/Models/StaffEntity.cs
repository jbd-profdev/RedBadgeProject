using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedBadgeProject.Server.Models
{
    public class StaffEntity
    {
        public StaffEntity()
        {
            TripList = new HashSet<TripEntity>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
        public virtual CompanyEntity? Company { get; set; }

        [ForeignKey(nameof(Location))]
        public int CurrentLocationId { get; set; }
        public virtual LocationEntity? Location { get; set; }

        public int RoleId { get; set; }

        public virtual ICollection<TripEntity> TripList { get; set; } = new List<TripEntity>();
    }
}