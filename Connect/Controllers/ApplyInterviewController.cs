using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Models;
using Connect.Models.Interview;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Connect.Controllers
{
    public class ApplyInterviewController : Controller
    {
        [HttpGet]
        [Route("AvailableInterviewSlots")]
        public ActionResult AvailableInterviewSlots(int campusSelection)
        {
            var model = new ApplyInterviewViewModel();
            model.AvailableInterviewSlots = new List<InterviewSlot>();
            List<InterviewSlot> interviewSlots = null;

            if (campusSelection != 0)
            {
                var campus = (Campus)campusSelection;
                if (campus != Campus.NONE)
                {
                    var batch = GetBatchFromCampus(campus.ToString());
                    interviewSlots = new InterviewRepository().GetAvailableInterviewSlots(batch);
                }
            }

            if (interviewSlots != null)
            {
                model.AvailableInterviewSlots = interviewSlots;
            }

            return PartialView("~/Views/Interview/ApplyInterviews.cshtml", model);
        }

        private string GetBatchFromCampus(string campus)
        {
            return ConfigurationManager.AppSettings["CourseCodeDatePart"] + campus;
        }
    }
}