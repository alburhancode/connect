using System;
using System.Configuration;

namespace Connect.Classes.Helpers
{
	public static class DeadlineHelper
	{
		public static bool IsDeadlineOn()
		{
			var deadlineOn = false;
			int year = int.Parse(ConfigurationManager.AppSettings["DeadlineYear"]);
			int month = int.Parse(ConfigurationManager.AppSettings["DeadlineMonth"]);
			int date = int.Parse(ConfigurationManager.AppSettings["DeadlineDate"]);

			var deadline = new DateTime(year, month, date);
			if (DateTime.Now > deadline)
				deadlineOn = true;

			return deadlineOn;
		}
	}
}