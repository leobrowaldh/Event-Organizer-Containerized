using Data.Context;
using Data.Models;

namespace Data.DataAccess
{
    public class DataAccess(EventOrganizerDbContext injectedContext) : IDataAccess
    {
        private readonly EventOrganizerDbContext _db = injectedContext;

		public Activity? GetActivity(int activityId) => _db.Activities.Find(activityId);

		public Event? GetEvent(Guid eventId) => _db.Events.Find(eventId);

        public ICollection<Activity> GetEventActivities(Guid eventId) => _db.Activities.Where(a => a.EventId == eventId).ToList();

        public ICollection<User> GetEventUsers(Guid eventId) => _db.Users.Where(u => u.EventId == eventId).ToList();

        public User? GetUser(int userId) => _db.Users.Find(userId);

        public bool PostActivity(Activity activity)
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
	}
}
