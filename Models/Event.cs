namespace EventsCalendar.Models
{
	public class Event
	{
		public uint Id { get; set; }
		public string Name { get; set; } = null!;
		public DateOnly Date { get; set; }
		public string Category { get; set; } = null!;
	}
	public class EventDescription
	{
		public uint Id { get; set; }
		public uint EventId { get; set; }
		public string Description { get; set; } = null!;
	}
}