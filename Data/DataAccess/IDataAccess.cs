using Data.Models;

namespace Data.DataAccess
{
    public interface IDataAccess
    {
        public ICollection<Activity> GetEventActivities(Guid eventId);
        public bool PostActivity(Activity activity);
        public bool PostEvent(Event ev);
        public Event? GetEvent(Guid eventId);
        public ICollection<User> GetEventUsers(Guid eventId);
        public User? GetUser(int userId);
        public bool PostUser(User user);
    }
}
