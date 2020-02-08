using System.Configuration;

namespace Connect.Classes.Helpers
{
	public static class UrlBuilder
	{
		public static string Url(string controller, string action)
		{
			return $"{ConfigurationManager.AppSettings["SiteRoot"]}/{controller}/{action}";
		}
	}
}