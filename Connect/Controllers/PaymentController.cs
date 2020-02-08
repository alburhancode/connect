using System;
using System.Threading;
using System.Web.Mvc;
using Connect.Classes;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models.Payment;
using System.Linq;

namespace Connect.Controllers
{
	[AuthorizeRole(UserType.PaymentAdministrator)]
    public class PaymentController : Controller
    {
        // GET: Payment
		[HttpGet]
		[Route("Get")]
        public ActionResult Get()
        {
            // get all payments that are due verification
            var payments = new IndividualRepository().GetPaymentsPendingVerificationAndDeclined();
            var pvm = new PaymentViewModel();
            pvm.Payments = payments;

            if (SessionHelper.LoggedInUser.Batch.Contains("-BW"))
            {
                return View("PaymentFemales", pvm);
            }
            else
            {
                return View("Payment", pvm);
            }
        }

		[HttpGet]
		[Route("UpdateStatus")]
		public ActionResult UpdateStatus(Guid id, string email, bool isPass, bool isBlocked = false)
		{
			var individualRepository = new IndividualRepository();

			var paymentStatus = isBlocked ? PaymentStatus.Blocked : (isPass ? PaymentStatus.Verified : PaymentStatus.Declined);
			individualRepository.UpdateIndividualPaymentVerifiedFlag(id, paymentStatus.ToString());

            if (SessionHelper.LoggedInUser.Batch.Contains("-BW"))
            {
                SendEmailToMastoorat(email, paymentStatus);
            }
            else
            {
                SendEmail(email, paymentStatus);
            }


            var payments = new IndividualRepository().GetPaymentsPendingVerificationAndDeclined();
			var pvm = new PaymentViewModel();
			pvm.Payments = payments;
			return View("Payment", pvm);
		}

        [HttpPost]
        [Route("remind-all-incomplete")]
        public PartialViewResult RemindAllInComplete()
        {
            var individualRepository = new IndividualRepository();
            var payments = individualRepository.GetPaymentsPendingVerificationAndDeclined().Where(i => i.RegistrationStatus == RegistrationStatus.Registered || i.RegistrationStatus == RegistrationStatus.UnRegistered);
            
            // get a list of individuals with incomplete status -- who need a reminder in this batch (session)
            foreach(var record in payments)
            {
                var paymentRepository = new PaymentRepository();
                SendInCompleteFormReminder(record.Email);
                paymentRepository.UpdateDateLastReminded(record.Id, DateTime.UtcNow);
            }
            // for each
            // call 

            payments = individualRepository.GetPaymentsPendingVerificationAndDeclined();

            var model = new PaymentViewModel();
            model.Payments = payments;
            return PartialView("~/Views/Payment/UnVerifiedPartial.cshtml", model);
        }

        [HttpGet]
        [Route("unverified-partial-get")]
        public PartialViewResult UnVerifiedPartialGet()
        {
            var individualRepository = new IndividualRepository();

            // This database call is inefficient because we only need UnVerified people
            var payments = new IndividualRepository().GetPaymentsPendingVerificationAndDeclined();

            var model = new PaymentViewModel();
            model.Payments = payments;
            return PartialView("~/Views/Payment/UnVerifiedPartial.cshtml", model);
        }

        [HttpPost]
        [Route("remind-single-ajax")]
        public ActionResult RemindSingleAjax(Guid id)
        {
            var paymentRepository = new PaymentRepository();
            paymentRepository.UpdateDateLastReminded(id, DateTime.UtcNow);

            var individual = new IndividualRepository().GetIndividualDetailsById(id);
            SendInCompleteFormReminder(individual.Email);

            var individualRepository = new IndividualRepository();

            // This database call is inefficient because we only need UnVerified people
            var payments = new IndividualRepository().GetPaymentsPendingVerificationAndDeclined();

            var model = new PaymentViewModel();
            model.Payments = payments;
            return PartialView("~/Views/Payment/UnVerifiedPartial.cshtml", model);
        }

        private static void SendInCompleteFormReminder(string email)
        {
            string bodyPending;
            string subjectPending;

            bodyPending =
                "Dear Applicant,\n\nYour application is not complete. Kindly share your education and professional details by logging into the portal at the following url https://www.alburhanconnect.com/authenticate For any query feel free to contact at 03332062067. \n\nRegards,\nAl-Burhan Admin Team";
            subjectPending = "Al-Burhan Application Status (InComplete)";

            new Thread(delegate () {
                EmailHelper.SendEmail(email, subjectPending, bodyPending);
            }).Start();
        }

        private static void SendEmailToMastoorat(string email, PaymentStatus paymentStatus)
        {
            string bodyPending;
            string subjectPending;
            if (paymentStatus == PaymentStatus.Verified)
            {
                bodyPending =
                    "Dear Applicant,\n\nYour application has been successfully received.\nAl-Burhan Women Team will contact you by 7th February.\nIn case of any query, feel free to contact at 03335551120 (Timings 09:00 AM to 01:00 PM)\n\nRegards,\nAl-Burhan";
                subjectPending = "Al-Burhan Application Status (Successful)";
            }
            else if (paymentStatus == PaymentStatus.Declined)
            {
                bodyPending =
                    "Dear Applicant,\n\nYour application is not successful. Kindly contact at 03335551120 (Timings 09:00 AM to 01:00 PM) for further details.\n\nRegards,\nAl-Burhan";
                subjectPending = "Al-Burhan Application Status (Unsuccessful)";
            }
            else
            {
                return;
            }

            new Thread(delegate () {
                EmailHelper.SendEmail(email, subjectPending, bodyPending);
            }).Start();
        }

        private static void SendEmail(string email, PaymentStatus paymentStatus)
	    {
		    string bodyPending;
		    string subjectPending;
		    if (paymentStatus == PaymentStatus.Verified)
		    {
			    bodyPending =
                    "Your application has been verified. Kindly appear for the interview at the selected time. In case of any query, feel free to contact 0333 2062067";
			    subjectPending = "Verification Successful";
		    }
		    else if(paymentStatus == PaymentStatus.Declined)
			{
			    bodyPending =
				    "Your Application was unsuccessful. Please contact at 0333 2062067 for details.";
			    subjectPending = "Verification Failure";
		    }
		    else if (paymentStatus == PaymentStatus.Blocked)
		    {
			    bodyPending =
                    "Your Application was unsuccessful. Please contact at 0333 2062067 for details.";
			    subjectPending = "Verification Failure";
		    }
		    else
		    {
			    return;
		    }

			new Thread(delegate () {
			    EmailHelper.SendEmail(email, subjectPending, bodyPending);
		    }).Start();
	    }
    }
}