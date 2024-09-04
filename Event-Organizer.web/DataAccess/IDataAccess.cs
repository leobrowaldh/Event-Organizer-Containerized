namespace Event_Organizer.web.DataAccess
{
	public interface IDataAccess
	{
		public ICollection<Activity> GetEventActivities(int eventId);
		public bool PostActivity(Activity activity);
		public bool PostEvent(Event ev); 
		public Event GetEvent(int eventId);
		public ICollection<User> GetEventUsers(int eventId);
		public User GetUser(int userId);
		public bool PostUser(User user);
	}
}
