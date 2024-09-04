using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        // Foreign Key
        [ForeignKey("Event")]
        public Guid EventId { get; set; }

        // Navigation properties
        public Event? Event { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
