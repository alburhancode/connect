using System.Web.Mvc;

namespace Connect.Controllers
{
    public class ErrorController : Controller
    {
		// GET: Error
		[Route("HttpError500")]
        public ActionResult HttpError500(string message = null)
		{
			var friendlyErrorMessage = ErrorMessage.GetFriendlyErrorMessage(message);
			var model = new ErrorViewModel();
			model.Message = friendlyErrorMessage;
            return View(model);
        }

	    [Route("HttpError403")]
	    public ActionResult HttpError403(string message = null)
	    {
		    var friendlyErrorMessage = ErrorMessage.GetFriendlyErrorMessage(message);
		    var model = new ErrorViewModel();
		    model.Message = friendlyErrorMessage;
		    return View(model);
	    }

		[Route("HttpError404")]
	    public ActionResult HttpError404(string message = null)
	    {
		    var friendlyErrorMessage = ErrorMessage.GetFriendlyErrorMessage(message);
		    var model = new ErrorViewModel();
		    model.Message = friendlyErrorMessage;
		    return View(model);
	    }

		[Route("error")]
	    public ActionResult Error(string message = null)
	    {
		    var friendlyErrorMessage = ErrorMessage.GetFriendlyErrorMessage(message);
		    var model = new ErrorViewModel();
		    model.Message = friendlyErrorMessage;
		    return View(model);
	    }
    }

	public static class ErrorMessage
	{
		internal const string MaxRequestSizeExceeded = "Maximum request length exceeded.";

		internal static string GetFriendlyErrorMessage(string message)
		{
			switch (message)
			{
				case MaxRequestSizeExceeded:
					return "A Maximum file size has exceeded. Please upload image of size maximum 1MB";
				default:
					return "An error has ocurred : " + message;
			}
		}
	}

	public class ErrorViewModel
	{
		public string Message { get; set; }
	}
}