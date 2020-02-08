using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;

namespace Connect
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		protected void Application_BeginRequest()
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
			Response.Cache.SetNoStore();
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError();
			Response.Clear();

			HttpException httpException = exception as HttpException;

			if (httpException != null)
			{
				string action;

				switch (httpException.GetHttpCode())
				{
					case 404:
						// page not found
						action = "HttpError404";
						break;
					case 500:
						// server error
						action = "HttpError500";
						break;
					default:
						action = "Error";
						break;
				}

				// clear error on server
				Server.ClearError();

				Response.Redirect($"~/{action}/?message={exception.Message}");
			}
		}
	}
}
