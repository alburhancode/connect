using Connect.Classes.Dapper;
using Connect.Classes.DataModels;
using Connect.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Connect.Controllers
{
    public class ApplyTimingsController : Controller
    {
        [HttpGet]
        [Route("AvailableBatchTimings")]
        public ActionResult AvailableBatchTimings(int campusSelection)
        {
            var model = new ApplyTimingViewModel();
            model.AvailableTimings = new List<BatchTiming>();
            List<BatchTiming> batchTimings = null;

            if (campusSelection != 0)
            {
                var campus = (Campus)campusSelection;
                if (campus != Campus.NONE)
                {
                    var batch = GetBatchFromCampus(campus.ToString());
                    batchTimings = new InterviewRepository().GetAvailableBatchTimings(batch);
                }
            }

            if (batchTimings != null)
            {
                model.AvailableTimings = batchTimings;
            }

            return PartialView("~/Views/Apply/ApplyTimings.cshtml", model);
        }

        [HttpGet]
        [Route("AvailableBatchTimingsByBatch")]
        public ActionResult AvailableBatchTimingsByBatch(string batch, Guid id)
        {
            var model = new ApplyTimingViewModel();
            model.AvailableTimings = new List<BatchTiming>();
            List<BatchTiming> batchTimings = null;

            var repo = new InterviewRepository();
            batchTimings = repo.GetAvailableBatchTimings(batch);
            model.SelectedBatchTimingId = repo.GetSelectedBatchTimingId(id);

            if (batchTimings != null)
            {
                model.AvailableTimings = batchTimings;
            }

            return PartialView("~/Views/Apply/ApplyTimings.cshtml", model);
        }

        private string GetBatchFromCampus(string campus)
        {
            return ConfigurationManager.AppSettings["CourseCodeDatePart"] + campus;
        }
    }
}