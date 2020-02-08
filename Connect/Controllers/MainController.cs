using System;
using System.Web.Mvc;
using Connect.Classes;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models.Interview;
using Connect.Models.Payment;
using Connect.Models.Preview;

namespace Connect.Controllers
{
	[AuthorizeRole(UserType.Applicant, UserType.Administrator, UserType.Panel, UserType.Ceo, UserType.PaymentAdministrator, UserType.Manager, UserType.AttendanceAdmin)]
    public class MainController : Controller
    {
	    [HttpGet]
	    [Route("preview-for-admin-partial")]
	    public PartialViewResult PreviewPartial(string id)
	    {
		    var individual = new IndividualRepository().GetIndividualDetailsById(new Guid(id));
		    var previewModel = FillModelFromEntity(individual);

		    if (individual.InterviewSlotId.HasValue && individual.InterviewSlotId > 0)
			    previewModel.InterviewSlot = new InterviewRepository().GetSelectedInterviewSlotById(individual.InterviewSlotId.Value);

			    return PartialView("~/Views/Preview/PreviewPartial.cshtml", previewModel);
	    }

	    [HttpGet]
	    [Route("ceo-grading-partial-get")]
	    public PartialViewResult CeoGradingPartialGet(string id)
	    {
		    var guid = new Guid(id);
			var model = new IndividualRepository().SelectCeoGrading(guid);
			if(model != null)
				model.Id = guid;

			return PartialView("~/Views/Grading/CeoGradingPartial.cshtml", model);
	    }

	    [HttpPost]
	    [Route("ceo-grading-partial")]
	    public JsonResult CeoGradingPartial(CeoGradingViewModel model)
	    {
		    new IndividualRepository().UpdateCeoGrading(model);
		    return Json(model);
	    }

	    [HttpGet]
	    [Route("panel-grading-partial-get")]
	    public PartialViewResult PanelGradingPartialGet(string id)
	    {
		    var guid = new Guid(id);
		    var model = new IndividualRepository().SelectPanelGrading(guid);
			if(model != null)
				model.Id = guid;

		    return PartialView("~/Views/Grading/PanelGradingPartial.cshtml", model);
	    }

		[HttpPost]
	    [Route("panel-grading-partial")]
	    public JsonResult PanelGradingPartial(PanelGradingViewModel model)
	    {
		    new IndividualRepository().UpdatePanelGrading(model);
		    return Json(model);
	    }

        [HttpGet]
        [Route("payments-partial-get")]
        public PartialViewResult PaymentPartialGet(string id)
        {
            var guid = new Guid(id);
            var individual = new IndividualRepository().GetIndividualBasicDetailsById(guid);
            var model = new PaymentDeclinedViewModel();
            if (individual != null)
            {                
                model.Id = guid;
                model.Name = individual.Name;
                model.PaymentStatus = individual.PaymentStatus;
                model.PaymentMethod = individual.PaymentMethod;

                if(model.PaymentMethod == "EasyPaisa")
                {
                    model.PaymentCodeEasyPaisa = long.Parse(individual.PaymentCode);
                }
                else if (model.PaymentMethod == "AlBurhan")
                {
                    model.PaymentCodeManualPreHyphen = int.Parse(individual.PaymentCode.Split('-')[0]);
                    model.PaymentCodeManualPostHyphen = int.Parse(individual.PaymentCode.Split('-')[1]);
                }
            }

            return PartialView("~/Views/Payment/PaymentPartial.cshtml", model);
        }

        [HttpPost]
        [Route("payments-partial")]
        public JsonResult PaymentsPartial(PaymentDeclinedViewModel model)
        {
            var individual = new Individual();

            individual.Id = model.Id;
            individual.PaymentMethod = model.PaymentMethod;
            individual.PaymentCode = ValidateAndGetPaymentCode(model.PaymentMethod, model.PaymentCodeManualPreHyphen,
                model.PaymentCodeManualPostHyphen, model.PaymentCodeEasyPaisa, SessionHelper.LoggedInUser.Email);

            individual.PaymentStatus = PaymentStatus.UnVerified;

            if (!ModelState.IsValid)
            {
                return Json(model);
            }

            new IndividualRepository().UpdatePaymentDetails(individual);
            return Json(new { ok = true, newurl = Url.Action("Get", "Payment") });
        }

        [HttpGet]
        [Route("notes-partial-get")]
        public PartialViewResult NotesPartialGet(string id)
        {
            var guid = new Guid(id);

            var model = new IndividualRepository().GetNotesForIndividual(guid);
            
            return PartialView("~/Views/Notes/NotesPartial.cshtml", model);
        }

        [HttpGet]
        [Route("callhistory-partial-get")]
        public PartialViewResult CallHistoryPartialGet(string id)
        {
            var guid = new Guid(id);

            var model = new IndividualRepository().GetCallHistoryForIndividual(guid);

            return PartialView("~/Views/CallHistory/CallHistoryPartial.cshtml", model);
        }

        [HttpGet]
        [Route("quizzes-partial-get")]
        public PartialViewResult QuizzesPartialGet(string id)
        {
            var guid = new Guid(id);

            var model = new IndividualRepository().GetQuizzesForIndividual(guid);

            return PartialView("~/Views/Quiz/QuizzesPartial.cshtml", model);
        }

//        [HttpPost]
//        [Route("notes-partial-delete")]
//        public JsonResult NotesPartialDelete(Note note)
//        {
//            // delete the note
//            new IndividualRepository().DeleteNote(note.NoteId);

////            var model = new IndividualRepository().GetNotesForIndividualById(note.IndividualId);
//            return Json(new { ok = true });
////            return PartialView("~/Views/Notes/NotesPartial.cshtml", model);
//        }

        [HttpPost]
        [Route("notes-partial-create")]
        public ActionResult NotesPartialCreate(Note note)
        {
            // delete the note
            new IndividualRepository().SaveNoteForIndividual(note.IndividualId, note.NoteText);
            var model = new IndividualRepository().GetNotesForIndividualById(note.IndividualId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Notes/NotesPartial.cshtml", model);
            }
            return View(model);
        }

        [HttpGet]
        [Route("attendance-partial-get")]
        public PartialViewResult AttendancePartialGet(string id)
        {
            var guid = new Guid(id);

            var model = new IndividualRepository().GetAttendanceForIndividual(guid);

            return PartialView("~/Views/Attendance/AttendancePartial.cshtml", model);
        }

        [HttpPost]
        [Route("attendance-partial-update")]
        public ActionResult AttendancePartialUpdate(IndividualAttendance attendance)
        {
            new IndividualRepository().UpdateAttendanceStatus(attendance.AttendanceId, attendance.Status);
            var model = new IndividualRepository().GetAttendanceForIndividual(attendance.Id);

            return PartialView("~/Views/Attendance/AttendancePartial.cshtml", model);
        }

        [HttpGet]
        [Route("leave-partial-get")]
        public PartialViewResult LeavePartialGet(string id)
        {
            var guid = new Guid(id);

            var model = new IndividualRepository().GetLeavesForIndividual(guid);

            return PartialView("~/Views/Leave/LeavePartial.cshtml", model);
        }

        [HttpPost]
        [Route("leave-partial-delete")]
        public ActionResult LeavePartialDelete(IndividualLeave leave)
        {
            // delete the note
            new IndividualRepository().DeleteLeave(leave.LeaveId);
            var model = new IndividualRepository().GetLeavesForIndividual(leave.Id);

            return PartialView("~/Views/Leave/LeavePartial.cshtml", model);
        }

        [HttpPost]
        [Route("leave-partial-create")]
        public ActionResult LeavePartialCreate(IndividualLeave leave)
        {
            new IndividualRepository().AddLeave(leave.IndividualId, leave.DateOfLeave, leave.Reason);
            var model = new IndividualRepository().GetLeavesForIndividual(leave.Id);

            return PartialView("~/Views/Leave/LeavePartial.cshtml", model);
        }

        #region private methods

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

        [HttpGet]
		[AuthorizeRole(UserType.PaymentAdministrator, UserType.Administrator, UserType.Panel, UserType.Applicant, UserType.Ceo, UserType.Manager, UserType.AttendanceAdmin)]
		[Route("preview-for-admin-main")]
		public ActionResult PreviewForAdmin(Guid id)
		{
            var previewModel = new PreviewViewModel();
            try
            {
                var individual = new IndividualRepository().GetIndividualDetailsById(id);
                previewModel = FillModelFromEntity(individual);

                if (individual.InterviewSlotId.HasValue && individual.InterviewSlotId > 0)
                    previewModel.InterviewSlot = new InterviewRepository().GetSelectedInterviewSlotById(individual.InterviewSlotId.Value);
            }
            catch (Exception e)
            {
                return RedirectToAction("error", "Error", new { message = e.ToString() });
            }

			return View("Main", previewModel);
		}

		private PreviewViewModel FillModelFromEntity(Individual individual)
		{
			var model = new PreviewViewModel();
			model.Individual = individual;
			return model;
		}

        #endregion
    }
}