namespace EventsCalendar.Models
{
	public class EventModel
	{
		public uint Id { get; set; }
		public string Name { get; set; } = null!;
		public DateOnly Date { get; set; }
		public string Category { get; set; } = null!;
		public string Description { get; set; } = null!;
	}
}