using Connect.Classes.Dapper;
using Connect.Models.DashBoard;
using System.Web.Mvc;

namespace Connect.Controllers
{
    [RoutePrefix("dashboard")]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [HttpGet]
        public ActionResult Index()
        {
            // Get all the messages with their replies by created date desc
            var repo = new DashboardRepository();
            var messages = repo.GetAllMessages();

            foreach(var message in messages)
            {
                message.Replies = new System.Collections.Generic.List<string>();
                message.Replies.AddRange(repo.GetAllReplies(message.MessageId));
            }

            return View(messages);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DashboardMessage message)
        {
            new DashboardRepository().InsertMessage(message);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var message = new DashboardRepository().GetMessage(id);
            return View(message);
        }

        [HttpPost]
        public ActionResult Edit(DashboardMessage message)
        {
            new DashboardRepository().UpdateMessage(message);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Reply(int id)
        {
            var message = new DashboardRepository().GetMessage(id);
            return View(message);
        }

        [HttpPost]
        public ActionResult Reply(DashboardMessage message)
        {
            new DashboardRepository().AddReplyToMessage(message);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            new DashboardRepository().DeleteMessage(id);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[Route("promote")]
        //public ActionResult Promote(int id)
        //{
        //    new DashboardRepository().DeleteMessage(id);
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //[Route("demote")]
        //public ActionResult Demote(int id)
        //{
        //    new DashboardRepository().DeleteMessage(id);
        //    return RedirectToAction("Index");
        //}
    }
}