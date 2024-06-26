using Microsoft.EntityFrameworkCore;

namespace EventsCalendar.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

		public DbSet<Event> Events { get; set; }
		public DbSet<EventDescription> EventDescriptions { get; set; }
	}
}