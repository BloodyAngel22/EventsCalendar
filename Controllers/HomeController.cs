using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventsCalendar.Models;

namespace EventsCalendar.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly AppDbContext _context;

	public HomeController(ILogger<HomeController> logger, AppDbContext context)
	{
		_logger = logger;
		_context = context;
	}

	public IActionResult Index()
	{
		var events = from ev in _context.Events
					join desc in _context.EventDescriptions
					on ev.Id equals desc.EventId
					select new
					{
						Id = ev.Id,
						Name = ev.Name,
						Date = ev.Date,
						Category = ev.Category,
						Description = desc.Description
					};

		List<EventModel> eventModels = new();
		foreach (var ev in events)
		{
			eventModels.Add(new EventModel
			{
				Id = ev.Id,
				Name = ev.Name,
				Date = ev.Date,
				Category = ev.Category,
				Description = ev.Description
			});
		}

		return View(eventModels);
	}

	[HttpPost]
	public async Task AddEvent([FromBody] EventModel eventModel)
	{
		_logger.LogInformation($"{eventModel.Id} {eventModel.Name} {eventModel.Date} {eventModel.Category}, {eventModel.Description}");

		var newEvent = new Event
		{
			Name = eventModel.Name,
			Date = eventModel.Date,
			Category = eventModel.Category
		};

		_context.Events.Add(newEvent);
		await _context.SaveChangesAsync();

		_context.EventDescriptions.Add(new EventDescription
		{
			EventId = newEvent.Id,
			Description = eventModel.Description
		});
		await _context.SaveChangesAsync();
	}

	[HttpPost]
	public async Task EditEvent([FromBody] EventModel eventModel)
	{
		_logger.LogInformation($"{eventModel.Id} {eventModel.Name} {eventModel.Date} {eventModel.Category}");

		var _event = _context.Events.Find(eventModel.Id);
		var eventDescription = _context.EventDescriptions.SingleOrDefault(e => e.EventId == eventModel.Id);

		if (_event == null || eventDescription == null)
		{
			_logger.LogWarning("Event or EventDescription not found");
			return;
		}

		_event.Name = eventModel.Name;
		_event.Date = eventModel.Date;
		_event.Category = eventModel.Category;
		eventDescription.Description = eventModel.Description;

		_context.Events.Update(_event);
		_context.EventDescriptions.Update(eventDescription);

		await _context.SaveChangesAsync();
	}

	[HttpPost]
	public async Task DeleteEvent([FromBody] uint Id)
	{
		_logger.LogInformation($"{Id}");
		var eventModel = _context.Events.Find(Id);
		var eventDescription = _context.EventDescriptions.SingleOrDefault(e => e.EventId == Id);

		if (eventModel == null || eventDescription == null)
		{
			_logger.LogWarning("Event or EventDescription not found");
			return;
		}

		_context.Events.Remove(eventModel!);
		_context.EventDescriptions.Remove(eventDescription!);
		await _context.SaveChangesAsync();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
