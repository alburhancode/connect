using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models.Quiz;
using System.Web.Mvc;

namespace Connect.Controllers
{
    public class AttendanceMainController : Controller
    {
        [AuthorizeRole(UserType.Panel, UserType.Ceo, UserType.Manager, UserType.PaymentAdministrator, UserType.Administrator, UserType.AttendanceAdmin)]
        public ActionResult Index()
        {
            var model = new OverallAttendanceViewModel();
            var overallAttendance = new AttendanceRepository().GetOverallAttendance(SessionHelper.LoggedInUser.Batch);

            model.OverallAttendance = overallAttendance;
            return View("AttendanceMain", model);
        }

        [HttpGet]
        [Route("individual-attendance-by-section-get")]
        public PartialViewResult AttendanceBySectionGet(string section = "A")
        {
            var model = new IndividualAttendanceBySectionViewModel();
            model.AttendanceSummary = new AttendanceRepository().GetIndividualAttendanceBySection(SessionHelper.LoggedInUser.Batch, section);
            
            return PartialView("~/Views/AttendanceMain/IndividualAttendanceBySection.cshtml", model);
        }

        //[HttpGet]
        //[Route("individual-attendance-by-subject-get")]
        //public PartialViewResult AttendanceBySubjectGet(string subject)
        //{
        //    var model = new IndividualAttendanceBySectionViewModel();
        //    model.AttendanceSummary = new AttendanceRepository().GetIndividualAttendanceBySubject(SessionHelper.LoggedInUser.Batch, subject);

        //    // call the repository to fetch data for attendance by section
        //    // set it on the model
        //    // changes to the view

        //    return PartialView("~/Views/AttendanceMain/IndividualAttendanceBySubject.cshtml", model);
        //}

        [HttpPost]
        [Route("create-calldetails-ajax")]
        public JsonResult CreateCallDetails(int individualid, string calloutcome, string callnotes, string caller, string kefiyat)
        {
            new AttendanceRepository().CreateCallDetailsForIndividual(individualid, calloutcome, callnotes, caller, kefiyat);
            return Json(true);
        }
    }
}