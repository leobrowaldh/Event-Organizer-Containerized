
namespace Event_Organizer.web.DataAccess
{
	public class DataAccess : IDataAccess
	{
		public Event GetEvent(int eventId)
		{
			throw new NotImplementedException();
		}

		public ICollection<Activity> GetEventActivities(int eventId)
		{
			List<Activity> activities = [new Activity() { Name = "Movies" }, new Activity() { Name = "Restaurant" }, new Activity() { Name = "Bar" }];
			return activities;
		}

		public ICollection<User> GetEventUsers(int eventId)
		{
			throw new NotImplementedException();
		}

		public User GetUser(int userId)
		{
			throw new NotImplementedException();
		}

		public bool PostActivity(Activity activity)
		{
			throw new NotImplementedException();
		}

		public bool PostEvent(Event ev)
		{
			throw new NotImplementedException();
		}

		public bool PostUser(User user)
		{
			throw new NotImplementedException();
		}
	}
}
