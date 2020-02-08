using System;
using System.Web.Mvc;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models.Preview;

namespace Connect.Controllers
{
	[AuthorizeRole(UserType.Applicant, UserType.Administrator, UserType.Panel, UserType.Ceo, UserType.PaymentAdministrator)]
    public class PreviewController : Controller
    {
        // GET: Preview
		[HttpGet]
		[Route("preview")]
        public ActionResult Preview()
		{
			var id = SessionHelper.LoggedInUser.Id;
	        var individual = new IndividualRepository().GetIndividualDetailsById(id);
			var previewModel = FillModelFromEntity(individual);

	        if (individual.InterviewSlotId.HasValue && individual.InterviewSlotId > 0)
		        previewModel.InterviewSlot = new InterviewRepository().GetSelectedInterviewSlotById(individual.InterviewSlotId.Value);

            return View("Preview", previewModel);
        }

	    // GET: Preview
	    [HttpGet]
		[AuthorizeRole(UserType.PaymentAdministrator, UserType.Administrator)]
	    [Route("preview-for-admin")]
	    public ActionResult PreviewForAdmin(Guid id)
	    {
		    var individual = new IndividualRepository().GetIndividualDetailsById(id);
		    var previewModel = FillModelFromEntity(individual);

		    if (individual.InterviewSlotId.HasValue && individual.InterviewSlotId > 0)
			    previewModel.InterviewSlot = new InterviewRepository().GetSelectedInterviewSlotById(individual.InterviewSlotId.Value);

		    return View("Preview", previewModel);
	    }

	    // GET: Preview
	    [HttpGet]
	    [AuthorizeRole(UserType.Panel)]
	    [Route("preview-for-panel")]
	    public ActionResult PreviewForPanel(Guid id)
	    {
		    var individual = new IndividualRepository().GetIndividualDetailsById(id);
		    var previewModel = FillModelFromEntity(individual);

		    if (individual.InterviewSlotId.HasValue && individual.InterviewSlotId > 0)
			    previewModel.InterviewSlot = new InterviewRepository().GetSelectedInterviewSlotById(individual.InterviewSlotId.Value);

		    return View("Preview", previewModel);
	    }

	    // GET: Preview
	    [HttpGet]
	    [AuthorizeRole(UserType.Ceo)]
	    [Route("preview-for-ceo")]
	    public ActionResult PreviewForCeo(Guid id)
	    {
		    var individual = new IndividualRepository().GetIndividualDetailsById(id);
		    var previewModel = FillModelFromEntity(individual);

		    if (individual.InterviewSlotId.HasValue && individual.InterviewSlotId > 0)
			    previewModel.InterviewSlot = new InterviewRepository().GetSelectedInterviewSlotById(individual.InterviewSlotId.Value);

		    return View("Preview", previewModel);
	    }

		private PreviewViewModel FillModelFromEntity(Individual individual)
		{
			var model = new PreviewViewModel();
			model.Individual = individual;
			return model;
		}
	}
}