using System.Configuration;
using System.Threading;
using System.Web.Mvc;
using Connect.Classes;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models.Payment;

namespace Connect.Controllers
{
    public class ConfirmationController : Controller
    {
		[HttpGet]
	    [Route("paymentbeingverified")]
	    public ActionResult PaymentBeingVerified()
	    {
		    return View();
	    }

		[HttpGet]
	    [Route("unverified")]
	    [AuthorizeRole(UserType.Applicant)]
	    public ActionResult UnVerified()
	    {
		    return View();
	    }

	    [HttpGet]
	    [Route("unregistered")]
	    [AuthorizeRole(UserType.Applicant)]
	    public ActionResult UnRegistered()
	    {
		    return View();
	    }

		[HttpPost]
	    [Route("unregistered")]
	    public ActionResult UnRegisteredSubmit()
	    {
			// Resend the activation email
			SendEmail(SessionHelper.LoggedInUser.Email, SessionHelper.LoggedInUser.Id.ToString());
			SessionHelper.Signout();
		    return RedirectToAction("Authenticate", "Login");
		    //return View("UnRegistered");
	    }

		[HttpGet]
	    [Route("submitted")]
	    public ActionResult Submitted()
	    {
		    return View();
	    }

		[HttpGet]
		[Route("deadlined")]
		public ActionResult Deadlined()
		{
			return View();
		}

		[HttpGet]
	    [Route("locked")]
	    public ActionResult Locked()
	    {
		    return View();
	    }

	    [HttpGet]
	    [Route("applylocked")]
	    public ActionResult ApplyLocked()
	    {
		    return View();
	    }

	    [HttpGet]
	    [Route("applyunverified")]
	    public ActionResult ApplyUnverified()
	    {
		    return View();
	    }

	    [HttpGet]
	    [Route("declined")]
	    [AuthorizeRole(UserType.Applicant)]
	    public ActionResult Declined()
	    {
		    return View();
	    }

	    [HttpPost]
	    [Route("declined")]
	    public ActionResult Declined(PaymentDeclinedViewModel model)
	    {
			var individual = new Individual();

			individual.Email = SessionHelper.LoggedInUser.Email;

		    individual.PaymentMethod = model.PaymentMethod;

			individual.PaymentCode = ValidateAndGetPaymentCode(model.PaymentMethod, model.PaymentCodeManualPreHyphen,
			    model.PaymentCodeManualPostHyphen, model.PaymentCodeEasyPaisa, SessionHelper.LoggedInUser.Email);

			individual.PaymentStatus = PaymentStatus.UnVerified;

			if (individual.PaymentMethod != "InterviewDay")
				model.Message = CheckIfPaymentCodeExist(individual.PaymentCode);

		    if (!ModelState.IsValid)
		    {
			    return View(model);
		    }

		    new IndividualRepository().UpdatePaymentDetails(individual);
		    SendEmail(individual.Email, individual.PaymentStatus);

			// positive case
			return RedirectToAction("UnVerified", "Confirmation");
	    }

		private string ValidateAndGetPaymentCode(string paymentMethod, int? paymentCodeManualPreHyphen, int? paymentCodeManualPostHyphen, long? paymentCodeEasyPaisa, string email)
	    {
		    string paymentCode = string.Empty;

		    if (paymentMethod == "AlBurhan")
		    {
			    if (paymentCodeManualPreHyphen.HasValue && paymentCodeManualPreHyphen.Value > 0 && paymentCodeManualPostHyphen.HasValue && paymentCodeManualPostHyphen.Value > 0)
				    paymentCode = paymentCodeManualPreHyphen + "-" + paymentCodeManualPostHyphen;
			    else
				    ModelState.AddModelError("PaymentRequired", "You have opted to make a payment at Al Burhan. Once you have paid, enter your receipt number");
		    }

		    if (paymentMethod == "EasyPaisa")
		    {
			    if (paymentCodeEasyPaisa.HasValue && paymentCodeEasyPaisa.Value > 0)
				    paymentCode = paymentCodeEasyPaisa.ToString();
			    else
				    ModelState.AddModelError("PaymentRequired", "You have opted to pay through Easy Paisa. Once you have paid, enter your transaction id");
		    }

		    return paymentCode;
	    }

	    private string CheckIfPaymentCodeExist(string paymentCode)
	    {
		    var errorMessage = "";
		    var isDuplicate = new IndividualRepository().DoesPaymentCodeExist(paymentCode);
		    if (isDuplicate)
		    {
			    errorMessage = "This payment code has already been used by another applicant.";
			    ModelState.AddModelError("DuplicatePaymentCode", errorMessage);
		    }
		    return errorMessage;
	    }

	    private static void SendEmail(string email, PaymentStatus paymentStatus)
	    {
		    string bodyPending;
		    string subjectPending;
		    if (paymentStatus == PaymentStatus.Verified)
		    {
			    bodyPending =
				    "Your payment verification was successful. You can now login and complete your application";
			    subjectPending = "Payment Verification Successful";
		    }
		    else
		    {
			    bodyPending =
				    "Your payment verification was unsuccessful. Please login to the site and try again.";
			    subjectPending = "Payment Verification Failure";
		    }

		    new Thread(delegate () {
			    EmailHelper.SendEmail(email, subjectPending, bodyPending);
		    }).Start();
	    }

		private static void SendEmail(string email, string id)
	    {
		    var subjectPending = "Thank you for applying.";
		    var siteAddress = ConfigurationManager.AppSettings["SiteAddress"];
		    var urlForNotVerified = siteAddress + $"/apply?id={id}";

		    string bodyPending;
		    bodyPending =
			    $"Your application has been received. Please click the link {urlForNotVerified} to activate your account. Once logged in, you will be required to complete your application";

		    new Thread(delegate () {
			    EmailHelper.SendEmail(email, subjectPending, bodyPending);
		    }).Start();
	    }
	}
}