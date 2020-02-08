using System.Web.Mvc;
using Connect.Classes.Dapper;

namespace Connect.Controllers
{
    public class ValidationController : Controller
    {
		[HttpGet]
		[Route("DoesEmailExist")]
	    public ActionResult DoesEmailExist(string email)
		{
			var emailAlreadyExists = new IndividualRepository().DoesEmailExist(email);
			return Json(!emailAlreadyExists, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
	    [Route("IsValidPaymentCombination")]
	    public ActionResult IsValidPaymentCombination(string paymentCode, bool isChecked)
	    {
		    var result = true;
		    if (isChecked)
		    {
			    if (!string.IsNullOrEmpty(paymentCode))
				    result = false;
		    }
		    return Json(result, JsonRequestBehavior.AllowGet);
	    }

		[HttpGet]
	    [Route("IsNicNumberValid")]
	    public ActionResult IsNicNumberValid(string nicNumber)
	    {
		    var isValid = false;
		    long result;
		    isValid = long.TryParse(nicNumber, out result);

		    return Json(isValid, JsonRequestBehavior.AllowGet);
	    }
    }
}