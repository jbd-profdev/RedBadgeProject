using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedBadgeProject.Server.Models
{
    public class TripEntity
    {
        public TripEntity()
        {
            StaffList = new HashSet<TripStaffEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(LocationFrom))]
        public int LocationFromId { get; set; }
        public virtual LocationEntity LocationFrom { get; set; }

        [ForeignKey(nameof(LocationTo))]
        public int LocationToId { get; set; }
        public virtual LocationEntity LocationTo { get; set; }

        [ForeignKey(nameof(Vehicle))]
        public int VehicleId { get; set; }
        public virtual VehicleEntity Vehicle { get; set; }

        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<TripStaffEntity> StaffList { get; set; } = new List<TripStaffEntity>();
    }
}