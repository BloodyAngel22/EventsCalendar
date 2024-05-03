namespace EventsCalendar.Models
{
	public static class Category
	{
		public const string Work = "Работа";
		public const string HomeWork = "Домашние дела";
		public const string Personal = "Личное";
		public const string Health = "Здоровье";
		public const string Holiday = "Праздники";

		public static string GetCategoryColor(string category) 
		{
			switch (category)
			{
				case Work:
					return "blue";
				case HomeWork:
					return "green";
				case Personal:
					return "orange";
				case Health:
					return "#eb0071";
				case Holiday:
					return "purple";
				default:
					return "black";
			}
		}
	}
}