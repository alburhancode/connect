using Connect.Classes.Dapper;
using Connect.Models.Charts;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Connect.Controllers
{
    public class ChartsController : Controller
    {
        // GET: ApplicationsChart
        [HttpGet]
        [Route("stats")]
        public ActionResult Index()
        {
            var model = new ApplicationChartsViewModel();
            var repo = new IndividualRepository();

            var ci = repo.GetApplicationChartData("DEC2019ISB");
            var ki = repo.GetApplicationChartData("DEC2019KHI");
            var li = repo.GetApplicationChartData("DEC2019LHR");
            var ciw = repo.GetApplicationChartData("DEC2019ISBW");

            model.Applications = new List<ApplicationChartData>();
            model.Applications.Add(new ApplicationChartData { Campus = "Islamabad", Count = ci });
            model.Applications.Add(new ApplicationChartData { Campus = "Karachi", Count = ki });
            model.Applications.Add(new ApplicationChartData { Campus = "Lahore", Count = li });
            model.Applications.Add(new ApplicationChartData { Campus = "Islamabad (W)", Count = ciw });

            return View("ApplicationCharts", model.Applications);
        }
    }
}