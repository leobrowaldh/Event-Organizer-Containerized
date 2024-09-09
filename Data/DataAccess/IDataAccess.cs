using Data.Models;

namespace Data.DataAccess
{
    public interface IDataAccess
    {
        public ICollection<Activity> GetEventActivities(Guid eventId);
        public Activity? GetActivity(int activityId);
        public bool PostActivity(Activity activity);
        public bool PostEvent(Event ev);
        public Event? GetEvent(Guid eventId);
        public ICollection<User> GetEventUsers(Guid eventId);
        public User? GetUser(int userId);
        public bool PostUser(User user);
		public void PutUser(User user);
        public bool UpdateActivity(Activity activity);
        public bool DeleteActivity(Activity activity);

    }
}
