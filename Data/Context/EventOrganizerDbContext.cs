using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class EventOrganizerDbContext : DbContext
    {
        public EventOrganizerDbContext(DbContextOptions<EventOrganizerDbContext> options) : base(options) { }

        // DbSet properties for each entity
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Activity> Activities => Set<Activity>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // An Event has many Activities, each Activity belongs to one Event
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Event)
                .WithMany(e => e.Activities)
                .HasForeignKey(a => a.EventId);

            // An Event has many Users, each User belongs to one Event
            modelBuilder.Entity<User>()
                .HasOne(u => u.Event)
                .WithMany(e => e.Users)
                .HasForeignKey(u => u.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // An Activity has many Users, each User belongs to one Activity
            modelBuilder.Entity<User>()
                .HasOne(u => u.Activity)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.ActivityId)
				.OnDelete(DeleteBehavior.SetNull);
		}


    }
}
