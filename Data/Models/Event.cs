using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public bool OpenForEditing { get; set; }

        // Navigation properties
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
