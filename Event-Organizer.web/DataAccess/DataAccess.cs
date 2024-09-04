
namespace Event_Organizer.web.DataAccess
{
	public class DataAccess : IDataAccess
	{
		public Event GetEvent(int eventId)
		{
			return new Event() { Name = "The mysterious event", OpenForEditing = true };
		}

		public ICollection<Activity> GetEventActivities(int eventId)
		{
			List<Activity> activities = [new Activity() { Name = "Movies", Description = "basd asd lnjasnd nklasknldnkals dknlasnd nklasnkld d asd " }, 
				new Activity() { Name = "Restaurant", Description = "basd asd lnjasnd nklasknldnkals dknlasnd nklasnkld d asd "  }, 
				new Activity() { Name = "Bar", Description = "basd asd lnjasnd nklasknldnkals dknlasnd nklasnkld d asd "  }];
			return activities;
		}

		public ICollection<User> GetEventUsers(int eventId)
		{
			return [new User() { Name = "Ahmed"}, new User() { Name = "Olle" }, new User() { Name = "Pepe" },
			new User() { Name = "Tobias"}, new User() { Name = "Yasmine"}, new User() { Name = "Martin"}];
		}

		public User GetUser(int userId)
		{
			return new User() { Name = "Olle" };
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
