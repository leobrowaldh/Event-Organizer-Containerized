using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public bool OpenForEditing { get; set; }

        // Navigation properties
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
