using System.Threading;
using System.Web.Mvc;
using Connect.Classes;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Models;

namespace Connect.Controllers
{
    public class LoginController : Controller
    {
		[HttpGet]
	    [Route("authenticate")]

	    public ActionResult Authenticate()
	    {
		    return View("Authenticate", new LoginViewModel());
	    }

	    [HttpPost]
	    [Route("authenticate")]
	    public ActionResult Authenticate(LoginViewModel loginViewModel)
	    {
			var repository = new IndividualRepository();

			if (ModelState.IsValid)
		    {
				var individual = repository.GetIndividualBasicDetailsByEmail(loginViewModel.Username);

			    if (individual != null && individual.Password == loginViewModel.Password)
			    {
				    SessionHelper.SetAuthCookie(loginViewModel.Username, false);
				    var loggedInUser = new LoggedInUser
				    {
						Id = individual.Id,
						Email = loginViewModel.Username,
					    FirstName = individual.Name,
					    UserType = individual.UserType,
                        Batch = individual.Batch,
                        Sections = individual.Sections,
                        Subject = individual.Subject,
                        Section = individual.Section
                    };
				    SessionHelper.LoggedInUser = loggedInUser;

                    var routeResult = GetActionForRedirect(individual);
                    if (routeResult != null)
                        return routeResult;
                }

                loginViewModel.HasAuthenticationFailed = true;
			}

			return View("authenticate", loginViewModel);
	    }



        public RedirectToRouteResult GetActionForRedirect(Individual individual)
        {
            if (individual.UserType == UserType.Applicant)
            {
                if (individual.RegistrationStatus == RegistrationStatus.UnRegistered)
                    return RedirectToAction("UnRegistered", "Confirmation");

                if (individual.PaymentStatus == PaymentStatus.Declined)
                    return RedirectToAction("Declined", "Confirmation");

                if (individual.RegistrationStatus == RegistrationStatus.Registered)
                    return RedirectToAction("Register", "Signup");

                if (individual.RegistrationStatus == RegistrationStatus.Completed
                    || individual.RegistrationStatus == RegistrationStatus.CeoInterviewed
                    || individual.RegistrationStatus == RegistrationStatus.PanelInterviewed
                    || individual.RegistrationStatus == RegistrationStatus.Enrolled)
                    return RedirectToAction("PreviewForAdmin", "Main", new { id = individual.Id });

                if (individual.RegistrationStatus == RegistrationStatus.Locked)
                    return RedirectToAction("Locked", "Confirmation");
            }
            //else if (individual.UserType == UserType.Administrator)
            //{
            //	return RedirectToAction("Records", "Home");
            //}
            else if (individual.UserType == UserType.Panel)
            {
                return RedirectToAction("GetIntervieweesForPanel", "Interview");
            }
            else if (individual.UserType == UserType.Ceo)
            {
                return RedirectToAction("GetIntervieweesForCeo", "Interview");
            }
            else if (individual.UserType == UserType.Manager)
            {
                return RedirectToAction("GetCandidatesForCeoDecision", "Admissions");
            }
            else if (individual.UserType == UserType.PaymentAdministrator)
            {
                return RedirectToAction("Get", "Payment");
            }
            else if (individual.UserType == UserType.QuizAdmin)
            {
                return RedirectToAction("Index", "Events");
            }
            else if (individual.UserType == UserType.AttendanceAdmin)
            {
                return RedirectToAction("Index", "AttendanceMain");
            }

            return null;
        }

        [HttpGet]
	    [Route("ForgotPassword")]
	    public ActionResult ForgotPassword()
	    {
		    return View();
	    }

		[HttpPost]
	    [Route("ForgotPassword")]
	    public ActionResult ForgotPassword(LoginViewModel model)
	    {
			// get details of the user based on their email address then send them the password
		    var repository = new IndividualRepository();
		    var individual = repository.GetIndividualBasicDetailsByEmail(model.ForgotPasswordUsername);
			if(individual != null)
				SendEmail(model.ForgotPasswordUsername, individual.Password);
			else
			{
				model.HasAuthenticationFailed = true;
				return View("ForgotPassword", model);
			}
			// send them their password
			return RedirectToAction("Authenticate", "Login");
	    }

	    private static void SendEmail(string email, string password)
	    {
		    var subjectPending = "Password Resent";

			string bodyPending;
		    bodyPending =
			    $"You asked us to resend your password to this e-mail address. Your password is {password}";

		    new Thread(delegate () {
			    EmailHelper.SendEmail(email, subjectPending, bodyPending);
		    }).Start();
	    }

		[Route("Login/RedirectToLogin")]
		public ActionResult RedirectToLogin()
	    {
		    return View("RedirectToLogin");
	    }

	    [Route("Logout")]
	    public ActionResult Logout()
	    {
		    SessionHelper.Signout();
		    return RedirectToAction("Authenticate", "Login");
	    }
    }
}