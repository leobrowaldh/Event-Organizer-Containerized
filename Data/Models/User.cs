using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? VotingColor { get; set; }

        // Foreign Keys
        [ForeignKey("Event")]
        public Guid EventId { get; set; }

        [ForeignKey("Activity")]
        public int? ActivityId { get; set; }

        // Navigation properties
        public Event? Event { get; set; }
        public Activity? Activity { get; set; }
    }
}
