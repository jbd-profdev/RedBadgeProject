using System.ComponentModel.DataAnnotations.Schema;

namespace RedBadgeProject.Server.Models
{
    public class TripStaffEntity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }
        public virtual StaffEntity Staff { get; set; } = null!;

        [ForeignKey(nameof(Trip))]
        public int TripId { get; set; }
        public virtual TripEntity Trip { get; set; } = null!;
    }
}