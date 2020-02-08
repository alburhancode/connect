using System;
using System.Configuration;
using System.Threading;
using System.Web.Mvc;
using Connect.Classes;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models;

namespace Connect.Controllers
{
    public class ApplyController : Controller
    {
	    // GET: Apply
	    [HttpGet]
	    [Route("apply")]
	    public ActionResult Apply(Guid? id = null, string method = null)
	    {
		    if (id == null || id == Guid.Empty) 
		    {
			    if (method != "internal")
			    {
				    var deadlineOn = DeadlineHelper.IsDeadlineOn();
				    if (deadlineOn)
					    return RedirectToAction("ApplyLocked", "Confirmation");
			    }
                else
                {
                    TempData["method"] = "internal";
                }
		    }

		    SessionHelper.Signout();
		    if (id != null && id != Guid.Empty)
		    {
				var isRegistered = new IndividualRepository().IsAccountVerified(id.Value.ToString());

			    // TODO: if the code is wrong or this person is blacklisted, then send to the unverified page. 
				if (isRegistered)
					return RedirectToAction("authenticate", "Login");
		    }

			var model = new ApplyViewModel();
			//model.AvailableInterviewSlots = new InterviewRepository().GetAvailableInterviewSlots();
			return View(model);
		}

        [HttpPost]
	    [Route("apply")]
	    public ActionResult Apply(ApplyViewModel applyViewModel)
	    {
            var method = TempData["method"];
            TempData["method"] = "";

            var respository = new IndividualRepository();
		    var id = Guid.NewGuid();
            var currentBatch = ConfigurationManager.AppSettings["CurrentBatch"];
            var individual = new Individual
            {
                Id = id,
                Name = applyViewModel.Name,
                Email = applyViewModel.Email,
                Password = applyViewModel.Password,
                Phone = applyViewModel.Phone,
                Age = applyViewModel.Age,
                UserType = UserType.Applicant,
                RegistrationStatus = RegistrationStatus.UnRegistered,
                PaymentMethod = applyViewModel.PaymentMethod,
                PaymentCode = ValidateAndGetPaymentCode(applyViewModel.PaymentMethod, applyViewModel.PaymentCodeManualPreHyphen, applyViewModel.PaymentCodeManualPostHyphen, applyViewModel.PaymentCodeEasyPaisa, applyViewModel.Email),
                PaymentStatus = PaymentStatus.UnVerified,
                CurrentStatus = applyViewModel.CurrentStatus,
                Batch = GetBatchFromCampus(applyViewModel.Campus.ToString()),
                Campus = applyViewModel.Campus == Campus.NONE ? Campus.KHI : applyViewModel.Campus
            };
			
            // TODO: Complete this
		    individual.InterviewSlotId = applyViewModel.SelectedInterviewSlotId;
            individual.BatchTimingId = applyViewModel.SelectedBatchTimingId;

            // This should be checked
            //if (individual.PaymentMethod == "InterviewDay")
            // individual.InterviewSlotId = 1033;      // None
            //else
            // applyViewModel.Message = CheckIfPaymentCodeExist(individual.PaymentCode);

            if (!ModelState.IsValid)
		    {
				//applyViewModel.AvailableInterviewSlots = new InterviewRepository().GetAvailableInterviewSlots();
				return View(applyViewModel);
		    }

            individual.Batch = GetBatchFromCampus(individual.Campus.ToString());
                       
            if(method != null && method.ToString() == "internal")
            {
                individual.RegistrationStatus = RegistrationStatus.Registered;
                respository.CreateOrUpdateBasicIndividual(individual);
                return RedirectToAction("Submitted", "Confirmation", new { applyViewModel.Name });
            }

            respository.CreateOrUpdateBasicIndividual(individual);
		    SendEmail(applyViewModel.Email, individual.Id.ToString());
		    return RedirectToAction("Submitted", "Confirmation", new { applyViewModel.Name });
	    }

        [HttpGet]
        [AuthorizeRole(UserType.Ceo)]
        [Route("applyadmin")]
        public ActionResult ApplyAdmin()
        {
            var model = new ApplyAdminViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("applyadmin")]
        public ActionResult ApplyAdmin(ApplyAdminViewModel applyViewModel)
        {
            var method = TempData["method"];
            TempData["method"] = "";

            var respository = new IndividualRepository();
            var id = Guid.NewGuid();
            var individual = new Individual
            {
                Id = id,
                Name = applyViewModel.Name,
                Email = applyViewModel.Email,
                Password = applyViewModel.Password,
                Phone = applyViewModel.Phone,
                Age = applyViewModel.Age,
                UserType = UserType.Applicant,
                RegistrationStatus = RegistrationStatus.Registered,
                PaymentStatus = PaymentStatus.Verified,
                CurrentStatus = applyViewModel.CurrentStatus,
                Batch = GetBatchFromCampus(applyViewModel.Campus.ToString()),
                Campus = applyViewModel.Campus
            };

            individual.BatchTimingId = applyViewModel.SelectedBatchTimingId;
            individual.InterviewSlotId = 0;

            if (!ModelState.IsValid)
            {
                return View(applyViewModel);
            }

            individual.Batch = GetBatchFromCampus(applyViewModel.Campus.ToString());

            respository.CreateOrUpdateBasicIndividual(individual);
            return RedirectToAction("GetIntervieweesForCeo", "Interview", new { applyViewModel.Name });
        }

        private string GetBatchFromCampus(string campus)
        {
            return ConfigurationManager.AppSettings["CourseCodeDatePart"] + campus;
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

		private static void SendEmail(string email, string id)
	    {
		    var subjectPending = "Thank you for applying.";
		    var siteAddress = ConfigurationManager.AppSettings["SiteAddress"];
		    var urlForNotVerified = siteAddress + $"/apply?id={id}";

		    string bodyPending;
			bodyPending =
				$"Your application has been received. Please click the link {urlForNotVerified} to activate your account. Once logged in, you will be required to complete your application";

			new Thread(delegate () {
				EmailHelper.SendEmail(email, subjectPending, bodyPending, ConfigurationManager.AppSettings["ActivationEmailCC"]);
			}).Start();
		}
    }
}