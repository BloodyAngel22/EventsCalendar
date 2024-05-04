using System.Text;

namespace EventsCalendar.Models
{
	public static class ConvertData
	{
		public static string EscapeUnicode(string input)
		{
			StringBuilder builder = new();
			foreach (char c in input)
			{
				if (c > 127)
				{
					builder.Append("\\u").Append(((int)c).ToString("x4"));
				}
				else
				{
					builder.Append(c);
				}
			}
			return builder.ToString();
		}

	}
}