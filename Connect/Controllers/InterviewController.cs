using System.Web.Mvc;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models.Interview;

namespace Connect.Controllers
{
	[AuthorizeRole(UserType.Panel, UserType.Ceo)]
	public class InterviewController : Controller
	{
        [HttpGet]
        [Route("GetIntervieweesForCeo")]
        [AuthorizeRole(UserType.Ceo)]
        public ActionResult GetIntervieweesForCeo()
        {
            // get all payments that are due verification
            var interviewes = new IndividualRepository().GetCompletedApplicationForIntervieweesForCeo();
            var pvm = new InterviewViewModel();
            pvm.Interviewees = interviewes;

            if (SessionHelper.LoggedInUser.Batch.Contains("-BW"))
            {
                return View("intervieweesForCeoFemales", pvm);
            }
            else
            {
                return View("intervieweesForCeo", pvm);
            }
        }

        // GET: Interview
        [HttpGet]
        [Route("GetIntervieweesForPanel")]
		[AuthorizeRole(UserType.Panel)]
		public ActionResult GetIntervieweesForPanel()
		{
			// get all payments that are due verification
			var interviewes = new IndividualRepository().GetCompletedApplicationForIntervieweesForPanel();
			var pvm = new InterviewViewModel();
			pvm.Interviewees = interviewes;

            if (SessionHelper.LoggedInUser.Batch.Contains("-BW"))
            {
                return View("intervieweesForPanelFemales", pvm);
            }
            else
            {
                return View("intervieweesForPanel", pvm);
            }

        }
	}
}