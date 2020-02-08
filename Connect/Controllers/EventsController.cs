using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Classes.Helpers;
using Connect.Classes.Security;
using Connect.Models.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Connect.Controllers
{
    [AuthorizeRole(UserType.Panel, UserType.Ceo, UserType.Manager, UserType.PaymentAdministrator, UserType.Administrator, UserType.QuizAdmin)]
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View("Events");
        }

        #region Get

        [HttpGet]
        public PartialViewResult ViewSelectedIndividualEvents(int id, string name, string rollNumber)
        {
            var events = new EventRepository().GetEventOccurencesForIndividual(id);
            Session["IndividualTitle"] = $"{name} - {rollNumber}";
            Session["IndividualId"] = id;
            return PartialView("~/Views/Events/IndividualEvents.cshtml", events);
        }

        [HttpGet]
        public PartialViewResult EventTypesGet()
        {
            var model = new EventRepository().GetEventTypes();
            return PartialView("~/Views/Events/EventTypes.cshtml", model);
        }

        [HttpGet]
        [Route("assessments-get")]
        public PartialViewResult EventOccurencesGet()
        {
            var model = new EventRepository().GetEventOccurences(SessionHelper.LoggedInUser.Batch);
            return PartialView("~/Views/Events/EventOccurences.cshtml", model);
        }

        [HttpGet]
        [Route("individuals-by-section-get")]
        public PartialViewResult IndividualsBySectionGet(string section, int selectedEvent = 0)
        {
            var attRepo = new AttendanceRepository();
            var model = new IndividualsBySectionViewModel();
            var eventRepo = new EventRepository();
            var events = eventRepo.GetEventOccurences(SessionHelper.LoggedInUser.Batch);
            var eventId = selectedEvent;

            var selectedEventExists = false;
            if(events.Count > 0) //&& events.Contains(e => e. selectedEvent > 0)
            {
                foreach(var eventItem in events)
                {
                    if(eventItem.EventId == selectedEvent)
                    {
                        selectedEventExists = true;
                        eventId = selectedEvent;
                        break;
                    }
                }

                if (!selectedEventExists)
                {
                    eventId = events[0].EventId;
                }
            }
            else
            {
                eventId = 0;
            }

            model.EventNames = new List<SelectListItem>();
            model.IndividualBasics = attRepo.GetIndividualsBySection(SessionHelper.LoggedInUser.Batch, section, eventId);

            foreach(var eventItem in events)
            {
                model.EventNames.Add(new SelectListItem() { Text = eventItem.EventName, Value = eventItem.EventId.ToString(), Selected = eventItem.EventId == eventId });
            }

            return PartialView("~/Views/Events/IndividualsBySection.cshtml", model);
        }

        #endregion

        #region CREATE

        #region Get

        [HttpGet]
        public PartialViewResult CreateAssessment()
        {
            var model = new CreateAssessmentViewModel();
            model.EventTypes = new EventRepository().GetEventTypes();
            return PartialView("~/Views/Events/CreateAssessment.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult CreateAssessmentType()
        {
            var model = new CreateAssessmentTypeViewModel();
            return PartialView("~/Views/Assessments/CreateAssessmentType.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult CreateIndividualEvent(int id)
        {
            var model = new CreateIndividualEventViewModel();
            model.EventOccurences = new EventRepository().GetEventOccurencesNotForIndividual(id);
            model.IndividualId = id;
            return PartialView("~/Views/Events/CreateIndividualEvent.cshtml", model);
        }

        #endregion

        #region Post

        [HttpPost]
        [Route("create-assessment-ajax")]
        public PartialViewResult CreateAssessmentAjax(CreateEventOccurenceViewModel assessment)
        {
            new EventRepository().CreateAssessment(assessment);
            var model = new EventRepository().GetEventOccurences(SessionHelper.LoggedInUser.Batch);
            return PartialView("~/Views/Events/EventOccurences.cshtml", model);
        }

        [HttpPost]
        [Route("create-assessment-type-ajax")]
        public PartialViewResult CreateAssessmentTypeAjax(CreateAssessmentTypeViewModel individualEvent)
        {
            new EventRepository().CreateAssessmentType(individualEvent);
            var model = new EventRepository().GetEventTypes();
            return PartialView("~/Views/Events/EventTypes.cshtml", model);
        }

        [HttpPost]
        [Route("create-individual-event-ajax")]
        public PartialViewResult CreateIndividualEventAjax(CreateIndividualEventViewModel individualEvent)
        {
            new EventRepository().CreateIndividualEvent(individualEvent);

            var events = new EventRepository().GetEventOccurencesForIndividual(Convert.ToInt32(Session["IndividualId"]));
            return PartialView("~/Views/Events/IndividualEvents.cshtml", events);
        }

        [HttpPost]
        [Route("create-or-update-or-delete-individual-quiz-marks-ajax")]
        public JsonResult CreateUpdateOrDeleteIndividualQuizMarks(int individualid, int eventid, int marks)
        {
            Thread.Sleep(100);
            new EventRepository().CreateUpdateOrDeleteIndividualQuizMarks(individualid, eventid, marks);

            //var events = new EventRepository().GetEventOccurencesForIndividual(Convert.ToInt32(Session["IndividualId"]));
            return Json(true); // new JsonResult();
            //PartialView("~/Views/Events/IndividualEvents.cshtml", events);
        }

        #endregion

        #endregion

        #region Delete

        [HttpGet]
        public PartialViewResult DeleteIndividualEvent(int id, int individualid)
        {
            new EventRepository().DeleteIndividualEvent(id);
            var events = new EventRepository().GetEventOccurencesForIndividual(individualid);
            return PartialView("~/Views/Events/IndividualEvents.cshtml", events);
        }

        [HttpGet]
        public PartialViewResult DeleteEventType(int id)
        {
            new EventRepository().DeleteEventType(id);
            var model = new EventRepository().GetEventTypes();
            return PartialView("~/Views/Events/EventTypes.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult DeleteAssessment(int id)
        {
            new EventRepository().DeleteEvent(id);
            var model = new EventRepository().GetEventOccurences(SessionHelper.LoggedInUser.Batch);
            return PartialView("~/Views/Events/EventOccurences.cshtml", model);
        }

        #endregion
    }
}