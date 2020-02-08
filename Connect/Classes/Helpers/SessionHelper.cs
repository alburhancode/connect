using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Connect.Classes.DataModels;

namespace Connect.Classes.Helpers
{
	public static class SessionHelper
	{
		public static LoggedInUser LoggedInUser
		{
			get
			{
				try
				{
					var loggedInUser = HttpContext.Current.Session["LoggedInUser"];
					if (loggedInUser != null && loggedInUser is LoggedInUser)
					{
						return (LoggedInUser)loggedInUser;
					}
				}
				catch (Exception)
				{
					// NLOG instead of throwing exception;
					throw new NullReferenceException();
				}

				return null;
			}
			set { HttpContext.Current.Session["LoggedInUser"] = value; }
		}

		public static void SetAuthCookie(string username, bool createPersistenceCookie)
		{
			FormsAuthentication.SetAuthCookie(username, createPersistenceCookie);
		}

		public static void Signout()
		{
			FormsAuthentication.SignOut();
			HttpContext.Current.Session["LoggedInUser"] = null;
		}

		public static bool IsLoggedIn(IPrincipal user)
		{
			return user.Identity.IsAuthenticated;
		}
	}
}