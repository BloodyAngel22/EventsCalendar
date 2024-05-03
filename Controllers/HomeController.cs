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
		return View(_context.Events.ToList());
	}

	[HttpPost]
	public async Task AddEvent([FromBody] Event eventModel)
	{
		_logger.LogInformation($"{eventModel.Id} {eventModel.Name} {eventModel.Date} {eventModel.Category}");
		_context.Events.Add(eventModel);
		await _context.SaveChangesAsync();
	}

	[HttpPost]
	public async Task EditEvent([FromBody] Event eventModel)
	{
		_logger.LogInformation($"{eventModel.Id} {eventModel.Name} {eventModel.Date} {eventModel.Category}");
		_context.Events.Update(eventModel);
		await _context.SaveChangesAsync();
	}

	[HttpPost]
	public async Task DeleteEvent([FromBody] uint Id)
	{
		_logger.LogInformation($"{Id}");
		var eventModel = _context.Events.Find(Id);
		_context.Events.Remove(eventModel!);
		await _context.SaveChangesAsync();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
