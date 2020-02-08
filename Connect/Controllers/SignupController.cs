using System;
using System.Web.Mvc;
using Connect.Classes;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models;

namespace Connect.Controllers
{
	[AuthorizeRole(UserType.Applicant, UserType.Panel, UserType.Ceo, UserType.PaymentAdministrator, UserType.Manager)]
    public class SignupController : Controller
    {
		[HttpGet]
	    [Route("register")]
	    public ActionResult Register(Guid? id)
		{
			Guid idInContext;

			if (id.HasValue)
			{
				if (SessionHelper.LoggedInUser.UserType == UserType.Applicant && SessionHelper.LoggedInUser.Id != id.Value)
				{
					return RedirectToAction("Authenticate", "Login");
				}

				idInContext = id.Value;
			}
			else
			{
				idInContext = SessionHelper.LoggedInUser.Id;
			}

			var model = new SignupViewModel();
			var repository = new InterviewRepository();

			var individualFromDb = new IndividualRepository().GetIndividualDetailsById(idInContext);

			// All the data needs to be filled into the model
			FillModelData(model, individualFromDb);

			model.AvailableInterviewSlots = new InterviewRepository().GetAvailableInterviewSlots(SessionHelper.LoggedInUser.Batch);
			model.SelectedInterviewSlotId = repository.GetSelectedInterviewSlot(idInContext);

            model.AvailableTimings = new InterviewRepository().GetAvailableBatchTimings(SessionHelper.LoggedInUser.Batch);
            model.SelectedBatchTimingId = repository.GetSelectedBatchTimingId(idInContext);

            if (SessionHelper.LoggedInUser.Batch.Contains("-BW") || SessionHelper.LoggedInUser.Batch.Contains("ISBW"))
            {
                return View("RegisterSisters", model);
            }
            else
            {
                return View(model);
            }
        }

		[HttpPost]
		[Route("register")]
		public ActionResult Register(SignupViewModel signupViewModel)
		{
			var respository = new IndividualRepository();
			var id = signupViewModel.Id;
			var individualFromDb = respository.GetIndividualDetailsById(id);
			FillFromModel(signupViewModel, individualFromDb);
			respository.UpdateIndividualDetails(individualFromDb);

			return RedirectToAction("PreviewForAdmin", "Main", new {id});
		}

		#region Private Methods

		private void FillFromModel(SignupViewModel model, Individual individualFromDb)
	    {
            if(model.NicNumber != null)
		        individualFromDb.NicNumber = model.NicNumber;

            if (model.OtherContact != null)
                individualFromDb.OtherContact = model.OtherContact;

                individualFromDb.MaritalStatus = model.MaritalStatus;

                individualFromDb.ResidentialAddress = model.ResidentialAddress;

                individualFromDb.PermanentAddress = model.PermanentAddress;

                individualFromDb.MatricGpa = model.MatricGpa;
                individualFromDb.MatricSubjects = model.MatricSubjects;
                individualFromDb.MatricInstitute = model.MatricInstitution;
                individualFromDb.IntermediateGpa = model.IntermediateGpa;
                individualFromDb.IntermediateSubjects = model.IntermediateSubjects;
                individualFromDb.IntermediateInstitute = model.IntermediateInstitution;
                individualFromDb.GraduationGpa = model.GraduationGpa;
                individualFromDb.GraduationSubjects = model.GraduationSubjects;
                individualFromDb.GraduationInstitute = model.GraduationInstitution;
                individualFromDb.MastersGpa = model.MastersGpa;
                individualFromDb.MastersSubjects = model.MastersSubjects;
                individualFromDb.MastersInstitution = model.MastersInstitution;
                individualFromDb.MsGpa = model.MsGpa;
                individualFromDb.MsSubjects = model.MsSubjects;
                individualFromDb.MsInstitution = model.MsInstitution;
                individualFromDb.PhdSubjects = model.PhdSubjects;
                individualFromDb.PhdInstitution = model.PhdInstitution;
                individualFromDb.OtherQualification = model.OtherQualification;

                individualFromDb.CurrentJob = model.CurrentJob;
                individualFromDb.CompanyName = model.CompanyName;
                individualFromDb.Designation = model.Designation;
                individualFromDb.TotalJobExperience = model.TotalJobExperience;

                individualFromDb.BusinessName = model.BusinessName;
                individualFromDb.BusinessArea = model.BusinessArea;
                individualFromDb.BusinessNature = model.BusinessNature;

                individualFromDb.AppliedBefore = model.AppliedBefore;

                individualFromDb.AnyReligiousEducation = model.AnyReligiousEducation;

                individualFromDb.ReligiousEducationSpecify = model.ReligiousEducationSpecify;
                individualFromDb.AnyOtherExpertise = model.AnyOtherExpertise;

                individualFromDb.CurrentStatus = model.CurrentStatus;

                individualFromDb.Education = model.Education;

            if(individualFromDb.RegistrationStatus == RegistrationStatus.UnRegistered || individualFromDb.RegistrationStatus == RegistrationStatus.Registered)
			    individualFromDb.RegistrationStatus = RegistrationStatus.Completed;

            //individualFromDb.Name = model.Name;
            if (model.Phone != null)
                individualFromDb.Phone = model.Phone;

            // new fields for sisters
            if (model.FatherOrHusbandName != null)
                individualFromDb.FatherOrHusbandName = model.FatherOrHusbandName;
            if (model.ClassSelection != null)
                individualFromDb.ClassSelection = model.ClassSelection;
            if (model.WhatsAppNumber != null)
                individualFromDb.WhatsAppNumber = model.WhatsAppNumber;
            if (model.EducationDetails != null)
                individualFromDb.EducationDetails = model.EducationDetails;
            if (model.Responsibilities != null)
                individualFromDb.Responsibilities = model.Responsibilities;
            if (model.ProfessionStudyBusinessDetails != null)
                individualFromDb.ProfessionStudyBusinessDetails = model.ProfessionStudyBusinessDetails;
            if (model.CityDetails != null)
                individualFromDb.CityDetails = model.CityDetails;

            if (model.SelectedBatchTimingId != 0)
                individualFromDb.BatchTimingId = model.SelectedBatchTimingId;

            if (!(SessionHelper.LoggedInUser.UserType == UserType.Applicant && individualFromDb.PaymentMethod == "InterviewDay" ))
				individualFromDb.InterviewSlotId = model.SelectedInterviewSlotId;
		}

		private void FillModelData(SignupViewModel model, Individual individual)
		{
			model.UserType = individual.UserType;
			model.Id = individual.Id;
			model.Name = individual.Name;
			model.Age = individual.Age;
			model.Email = individual.Email;
			model.Phone = individual.Phone;
			model.IndividualId = individual.IndividualId.ToString();
			model.NicNumber = individual.NicNumber;
			model.OtherContact = individual.OtherContact;
			model.MaritalStatus = individual.MaritalStatus;
			model.ResidentialAddress = individual.ResidentialAddress;
			model.PermanentAddress = individual.PermanentAddress;
			model.MatricGpa = individual.MatricGpa;
			model.MatricSubjects = individual.MatricSubjects;
			model.MatricInstitution = individual.MatricInstitute;
			model.IntermediateGpa = individual.IntermediateGpa;
			model.IntermediateSubjects = individual.IntermediateSubjects;
			model.IntermediateInstitution = individual.IntermediateInstitute;
			model.GraduationGpa = individual.GraduationGpa;
			model.GraduationSubjects = individual.GraduationSubjects;
			model.GraduationInstitution = individual.GraduationInstitute;
			model.MastersGpa = individual.MastersGpa;
			model.MastersSubjects = individual.MastersSubjects;
			model.MastersInstitution = individual.MastersInstitution;
			model.MsGpa = individual.MsGpa;
			model.MsSubjects = individual.MsSubjects;
			model.MsInstitution = individual.MsInstitution;
			model.PhdSubjects = individual.PhdSubjects;
			model.PhdInstitution = individual.PhdInstitution;
			model.OtherQualification = individual.OtherQualification;

			model.CurrentJob = individual.CurrentJob;
			model.CompanyName = individual.CompanyName;
			model.Designation = individual.Designation;
			model.TotalJobExperience = individual.TotalJobExperience;

			model.BusinessName = individual.BusinessName;
			model.BusinessArea = individual.BusinessArea;
			model.BusinessNature = individual.BusinessNature;

			model.AppliedBefore = individual.AppliedBefore;
			model.AnyReligiousEducation = individual.AnyReligiousEducation;
			model.ReligiousEducationSpecify = individual.ReligiousEducationSpecify;
			model.AnyOtherExpertise = individual.AnyOtherExpertise;
			model.CurrentStatus = individual.CurrentStatus;
			model.Education = individual.Education;

			model.PaymentMethod = individual.PaymentMethod;
            model.RegistrationStatus = individual.RegistrationStatus;
            model.Phone = individual.Phone;

            // new fields for sisters
            model.FatherOrHusbandName = individual.FatherOrHusbandName;
            model.ClassSelection = individual.ClassSelection;
            model.WhatsAppNumber = individual.WhatsAppNumber;
            model.EducationDetails = individual.EducationDetails;
            model.Responsibilities = individual.Responsibilities;
            model.ProfessionStudyBusinessDetails = individual.ProfessionStudyBusinessDetails;
            model.CityDetails = individual.CityDetails;
        }

        #endregion
    }
}