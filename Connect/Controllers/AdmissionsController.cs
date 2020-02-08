using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Connect.Classes;
using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models.Admissions;

namespace Connect.Controllers
{
    [AuthorizeRole(UserType.Ceo, UserType.Manager)]
    public class AdmissionsController : Controller
    {
        // GET: Interview
        [HttpGet]
        [Route("GetCandidatesForCeoDecision")]          // candidates that had their CEO interview done
        public ActionResult GetCandidatesForCeoDecision()
        {

            IEnumerable<Admission> interviewes = new List<Admission>();
            // get all payments that are due verification
            if (SessionHelper.LoggedInUser.Batch.Contains("-BW"))
            {
                interviewes = new IndividualRepository().GetCeoInterviewedFemaleCandidatesForFinalDecision();
            }
            else
            {
                interviewes = new IndividualRepository().GetCeoInterviewedCandidatesForFinalDecision();
            }

            var pvm = new AdmissionViewModel();
            pvm.Admissions = interviewes;
            return View("admission", pvm);
        }

        [HttpGet]
        [Route("GetOrderedEnrolledCandidates")]          // candidates that had their CEO interview done
        public PartialViewResult GetOrderedEnrolledCandidates(string orderBy = "age")
        {
            IEnumerable<Admission> interviewes = null;
            interviewes = new IndividualRepository().GetCeoInterviewedCandidatesForFinalDecision().Where(c => c.RegistrationStatus == RegistrationStatus.Enrolled);

            var pvm = new AdmissionViewModel();
            pvm.Admissions = interviewes;

            PartialViewResult pv = null;
            pv = PartialView("~/Views/Admissions/Enrolled.cshtml", pvm);
            return pv;
        }

        [HttpPost]
        [Route("update-section")]
        public JsonResult UpdateSectionAjax(string ids, string section)
        {
            var repository = new IndividualRepository();

            if (ids != null)
            {
                var idArray = ids.Split(',');
                for (int i = 0; i < idArray.Length; i++)
                {
                    // Update the Section on the Individual
                    repository.UpdateIndividualSection(int.Parse(idArray[i]), section);
                }
            }

            var interviewes = repository.GetCeoInterviewedCandidatesForFinalDecision();
            var countEmailsnotSent = interviewes.Count(c => c.RegistrationStatus == RegistrationStatus.Enrolled && !c.EmailSent);
            var countSectionsNotAssigned = interviewes.Count(c => c.RegistrationStatus == RegistrationStatus.Enrolled && (string.IsNullOrEmpty(c.Section) || c.Section.ToLower() == "none" || c.Section.ToLower() == "notselected"));

            return Json(new { ok = true, emails = countEmailsnotSent, sections = countSectionsNotAssigned });
        }

        [HttpPost]
        [Route("send-email")]
        public JsonResult SendEmailAjax(string ids)
        {
            var repository = new IndividualRepository();

            if (ids != null)
            {
                var idArray = ids.Split(',');
                for (int i = 0; i < idArray.Length; i++)
                {
                    var csvEmailAndSectionAndRollNumber = repository.UpdateIndividualEnrolmentEmailSentStatus(int.Parse(idArray[i]));
                    if(!string.IsNullOrEmpty(csvEmailAndSectionAndRollNumber) && csvEmailAndSectionAndRollNumber.Contains(","))
                        SendEmail(csvEmailAndSectionAndRollNumber.Split(',')[0], $"{csvEmailAndSectionAndRollNumber.Split(',')[1]}"
                            , $"{SessionHelper.LoggedInUser.Batch}-{csvEmailAndSectionAndRollNumber.Split(',')[2]}"
                            , SessionHelper.LoggedInUser.Batch);
                }
            }

            var interviewes = repository.GetCeoInterviewedCandidatesForFinalDecision();
            var countEmailsnotSent = interviewes.Count(c => c.RegistrationStatus == RegistrationStatus.Enrolled && !c.EmailSent);
            var countSectionsNotAssigned = interviewes.Count(c => c.RegistrationStatus == RegistrationStatus.Enrolled && (string.IsNullOrEmpty(c.Section) || c.Section.ToLower() == "none" || c.Section.ToLower() == "notselected"));

            return Json(new { ok = true, emails = countEmailsnotSent, sections = countSectionsNotAssigned });
        }

        [HttpPost]
        [Route("decision-partial")]
        public JsonResult PaymentsPartial(string ids, string decision)
        {
            var regStatus = RegistrationStatus.CeoInterviewed;
            switch (decision)
            {
                case "enrolled":
                    regStatus = RegistrationStatus.Enrolled;
                    break;
                case "rejected":
                    regStatus = RegistrationStatus.Rejected;
                    break;
                case "dropped":
                    regStatus = RegistrationStatus.Dropped;
                    break;
                default:
                    regStatus = RegistrationStatus.CeoInterviewed;
                    break;
            }

            var repository = new IndividualRepository();

            if(ids != null)
            {
                var idArray = ids.Split(',');
                for (int i = 0; i < idArray.Length; i++)
                {
                    var email = repository.UpdateIndividualStatus(int.Parse(idArray[i]), regStatus);

                    if (regStatus != RegistrationStatus.CeoInterviewed && regStatus != RegistrationStatus.Dropped)
                        SendEmail(email, regStatus == RegistrationStatus.Enrolled ? true : false, SessionHelper.LoggedInUser.Batch);
                }
            }

            return Json(new { ok = true });
        }

        private static void SendEmail(string email, string section, string rollNumber, string batch)
        {
            var subjectPending = "Important Information about your course";
            var siteAddress = ConfigurationManager.AppSettings["SiteAddress"];

            string bodyPending = $"Dear Alburhanian, \n\nRoll Number : {rollNumber}\n\nSection : {section}\n\n" +
                $"Attendance is mandatory. Please get yourself ready for one year " +
                $"Ilm-Deen course. \n\nIf your circumstances change or need any further details, feel free to contact Alburhan Team (0302 410 3622).";

            if (batch.ToLower() == "dec2019lhr")
            {
                bodyPending = $"Dear Alburhanian, \n\nRoll Number : {rollNumber}\n\nSection : {section}\n\n" +
                    $"Classes will commence on the 21st of December 2019. Attendance is mandatory. Please get yourself ready for one year " +
                    $"Ilm-Deen course. \n\nIf your circumstances change or need any further details, feel free to contact Alburhan Team (0302 410 3622).";
            }
            else if (batch.ToLower() == "dec2019isb")
            {
                bodyPending = $"Dear Alburhanian, \n\nRoll Number : {rollNumber}\n\nSection : {section}\n\n" +
                    $"Classes will commence on the 28th of December 2019. Attendance is mandatory. Please get yourself ready for one year " +
                    $"Ilm-Deen course. \n\nIf your circumstances change or need any further details, feel free to contact Alburhan Team (0302 410 3622).";
            }
            else if (batch.ToLower() == "dec2019khi")
            {
                bodyPending = $"Dear Alburhanian, \n\nRoll Number : {rollNumber}\n\nSection : {section}\n\n" +
                    $"Classes will commence on the 5th of January 2020. Attendance is mandatory. Please get yourself ready for one year " +
                    $"Ilm-Deen course. \n\nIf your circumstances change or need any further details, feel free to contact Alburhan Team (0302 410 3622).";
            }
            else if (batch.ToLower() == "dec2019isbw")
            {
                bodyPending = $"Dear Alburhanian, \n\nRoll Number : {rollNumber}\n\nSection : {section}\n\n" +
                    $"Classes will commence on the 28th of December 2019. Attendance is mandatory. Please get yourself ready for one year " +
                    $"Ilm-Deen course. \n\nIf your circumstances change or need any further details, feel free to contact Alburhan Team (0302 410 3622).";
            }

            new Thread(delegate () {
                EmailHelper.SendEmail(email, subjectPending, bodyPending, string.IsNullOrEmpty(ConfigurationManager.AppSettings["DecisionEmailCC"]) ? "" : ConfigurationManager.AppSettings["DecisionEmailCC"]);
            }).Start();
        }

        private static void SendEmail(string email, bool enrolled, string batch)
        {
            var subjectPending = "Al-Burhan Final decision on your application";
            //var isWomenBatch = batch.Contains("-BW");
            //if (isWomenBatch)
            //{
            //    subjectPending = "Application for Al-Burhan Women Course";
            //}

            var siteAddress = ConfigurationManager.AppSettings["SiteAddress"];

            string bodyPending;
            if (enrolled)
            {
                //if(isWomenBatch)
                //{
                //    bodyPending =
                //        $"Congratulations! We are pleased to announce that your application for Al-Burhan Women Course has been successful. \nPlease contact AlBurhan Team (0333 555 1120) for further coordination.";
                //}
                //else
                //{
                    bodyPending =
                        $"Congratulations! We are pleased to announce that your application has been successful. \nPlease contact AlBurhan Team (0302 410 3622) for further coordination.";

                if (batch.ToLower() == "dec2019lhr")
                {
                    bodyPending =
                        $"Congratulations! We are pleased to announce that your application has been successful. \nClasses will commence on 21st December 2019 as per the time specified at the time of application (Morning 8am / Evening 2 pm). The venue is Masjid Shane-e-Islam, 29-B/2, Gulberg 3, Lahore, Pakistan. \nPlease contact AlBurhan Team (0302 410 3622) for further coordination.";
                }
                else if (batch.ToLower() == "dec2019isb")
                {
                    bodyPending =
                        $"Congratulations! We are pleased to announce that your application has been successful. \nClasses will commence on 28th December 2019 as per the time specified at the time of application (Morning 9AM / Evening 2PM). The venue is Masjid Khatam-e-Nabbuwat, Street # 50, Sector F10/4, Islamabad, Pakistan. \n\nRegards, \n Al-Burhan Admission Team \n0518430843\n0333 2062067";
                }
                else if (batch.ToLower() == "dec2019khi")
                {
                    bodyPending =
                        $"Congratulations! We are pleased to announce that your application has been successful. \nClasses will commence on 4th January 2020 as per the time specified at the time of application (Morning 8AM / Evening 2PM). The venue is Plot # B-375, Federal B Area, Block-10 Gulberg Town, Karachi, Pakistan. \n\nRegards, \n Al-Burhan Admission Team \n0518430843\n0333 2062067";
                }
                else if (batch.ToLower() == "dec2019isbw")
                {
                    bodyPending =
                        $"Congratulations! We are pleased to announce that your application has been successful. \nClasses will commence on 28th December 2019 as per the time specified at the time of application (Morning 8am / Evening 2 pm). The venue is House # 173. Street # 48, Sector F10/4, islamabad, Pakistan. \nPlease contact AlBurhan Team (0302 410 3622) for further coordination.";
                }
            }
            else
            {
                //if(isWomenBatch)
                //{
                //    bodyPending =
                //        $"We're sorry to announce that your application was not successful on this ocassion. Kindly contact AlBurhan Team (0333 555 1120) for coordination.";
                //}
                //else
                //{
                    bodyPending =
                        $"We really appreciate that you took the time to consider applying at Al-Burhan. We would like to inform you that candidate selection is a relative process and your application was not successful this time. \n\nWe would like to keep in touch with you via the WhatsApp group. We hope you won't mind if we reach out to you in the future.\n\nKindly contact Al-Burhan Admission team for any query. JazakAllah\n\nRegards,\nAl-Burhan Admission Team\n051 8430843\n0333 2062067";
                //}
            }

            new Thread(delegate () {
                EmailHelper.SendEmail(email, subjectPending, bodyPending, string.IsNullOrEmpty(ConfigurationManager.AppSettings["DecisionEmailCC"]) ? "" : ConfigurationManager.AppSettings["DecisionEmailCC"]);
            }).Start();
        }
    }
}