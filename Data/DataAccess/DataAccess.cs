using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.DataAccess
{
    public class DataAccess(EventOrganizerDbContext injectedContext) : IDataAccess
    {
        private readonly EventOrganizerDbContext _db = injectedContext;

        public Models.Activity? GetActivity(int activityId) => _db.Activities.Find(activityId);

        public Event? GetEvent(Guid eventId) => _db.Events.Find(eventId);

        public ICollection<Models.Activity> GetEventActivities(Guid eventId)
        {
            return _db.Activities
                       .Include(a => a.Users)
                       .Where(a => a.EventId == eventId)
                       .ToList();
        }

        public ICollection<User> GetEventUsers(Guid eventId) => _db.Users.Where(u => u.EventId == eventId).ToList();

        public User? GetUser(int userId) => _db.Users.Find(userId);

        public bool PostActivity(Models.Activity activity)
        {
            // save activity to db
            _db.Activities.Add(activity);
            _db.SaveChanges();
            return true;
        }


        public bool PostEvent(Event ev)
        {
            //save event to db
            _db.Events.Add(ev);
            _db.SaveChanges();
            return true;
        }

        public bool PostUser(User user)
        {
            // save user to db
            _db.Users.Add(user);
            _db.SaveChanges();
            return true;
        }

        public void PutUser(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public bool UpdateActivity(Models.Activity activity)
        {
            // Update activity in db
            _db.Activities.Update(activity);
            _db.SaveChanges();
            return true;
        }

        public bool DeleteActivity(Models.Activity activity)
        {
            // Remove activity from db
            _db.Activities.Remove(activity);
            _db.SaveChanges();
            return true;
        }

        public Models.Activity? GetActivityWithUsers(int activityId)
        {
            return _db.Activities
                           .Include(a => a.Users) // Include associated users
                           .FirstOrDefault(a => a.Id == activityId);
        }

        public void UpdateEvent(Event updatedEvent)
        {
            _db.Events.Update(updatedEvent);
            _db.SaveChanges();
        }
    }
}
