using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connect.Classes.DataModels;

namespace Connect.Classes.Security
{
	public class AuthorizeRoleAttribute : AuthorizeAttribute
	{
		public AuthorizeRoleAttribute(params object[] roles)
		{
			if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
				throw new ArgumentException("The given role is not valid");

			Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (httpContext.Session["LoggedInUser"] != null)
			{
				var loggedInUser = (LoggedInUser)httpContext.Session["LoggedInUser"];
				var role = loggedInUser.UserType;
				return Roles.Contains(role.ToString());
			}
			return false;
		}
	}
}