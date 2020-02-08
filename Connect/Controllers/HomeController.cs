using System;
using System.Web.Mvc;
using Connect.Classes;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Security;
using Connect.Models.Records;

namespace Connect.Controllers
{
	[AuthorizeRole(UserType.Administrator, UserType.Panel, UserType.Ceo)]
	public class HomeController : Controller
	{
		[Route("~/")]
		public ActionResult Index()
		{
			// TODO: Implement Authenticate Attribute to find out if the user is logged in or not and then accordingly redirect to the login page or stay on the home page
			// This Authenticate attribute should be on top of each of the Get methods
			// until then, ?
			return RedirectToAction("Apply", "Apply");
		}

		[Route("Records")]
		public ActionResult Records()
		{
			var recordsViewModel = new RecordsViewModel();
			recordsViewModel.Individuals = new IndividualRepository().GetIndividuals();

			return View(recordsViewModel);
		}

		[HttpGet]
		[Route("pass")]
		public ActionResult Pass(Guid id)
		{
			var individualRepository = new IndividualRepository();
			individualRepository.UpdateIndividualState(id, RegistrationStatus.Enrolled.ToString());

			var recordsViewModel = new RecordsViewModel();
			recordsViewModel.Individuals = new IndividualRepository().GetIndividuals();
			return View("Records", recordsViewModel);
		}

		[HttpGet]
		[Route("undopass")]
		public ActionResult UndoPass(Guid id)
		{
			var individualRepository = new IndividualRepository();
			individualRepository.UpdateIndividualState(id, RegistrationStatus.Locked.ToString());

			var recordsViewModel = new RecordsViewModel();
			recordsViewModel.Individuals = new IndividualRepository().GetIndividuals();
			return View("Records", recordsViewModel);
		}

		//[Route("AllApplicants")]
		//public ActionResult AllApplicants()
		//{
		//	var recordsViewModel = new RecordsViewModel();
		//	recordsViewModel.Individuals = new IndividualRepository().GetIndividuals();

		//	return View(recordsViewModel);
		//}

		//[Route("EditRecord")]
		//[HttpGet]
		//public ActionResult EditRecord(Guid id)
		//{
		//	var recordViewModel = new RecordViewModel();
		//	recordViewModel.Individual = new IndividualRepository().GetIndividualDetailsById(id);
		//	return View(recordViewModel);
		//}

		//[Route("Delete")]
		//public ActionResult Delete(Guid id)
		//{
		//	var repository = new IndividualRepository();
		//	repository.DeleteIndividual(id);

		//	var recordsViewModel = new RecordsViewModel();
		//	recordsViewModel.Individuals = new IndividualRepository().GetIndividuals();

		//	return View("Records", recordsViewModel);
		//}
	}
}