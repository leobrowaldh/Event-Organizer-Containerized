namespace Event_Organizer.web.DataAccess
{
	public class Event
	{
		public string Name { get; set; }
		public bool OpenForEditing { get; set; }
	}
	public class Activity
	{
        public string Name { get; set; }
		public string Description { get; set; }
    }
	public class User
	{
		public string Name { get; set; }
	}
}
