using Connect.Classes;
using Connect.Classes.Dapper;
using Connect.Classes.Enums;
using Connect.Models.Grading.Api;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Connect.Controllers
{
    [RoutePrefix("api/v1")]
    public class PaymentsApiController : ApiController
    {
        [HttpGet]
        [Route("list/individuals/{cnic}")]
        public IHttpActionResult Get(string cnic)
        {
            var repo = new IndividualRepository();
            var individuals = repo.GetIndividualDetailsByCnic(cnic);

            return Ok(individuals);
        }

        [HttpGet]
        [Route("list/verified/{batch}/{authority}")]
        public IHttpActionResult Get(string batch, string authority)
        {
            var repo = new PaymentRepository();
            var individuals = repo.GetIndividualsWithPaymentVerifiedByBatch(batch, authority);

            return Ok(individuals);
        }

        private bool Authenticate(string token)
        {
            return token == ConfigurationManager.AppSettings["PaymentsAuthenticationToken"];
        }

        [HttpPost]
        [Route("grading/save/{authority}/{token}")]
        public HttpResponseMessage CreatePanelGrade(string authority, string token, [FromBody]GradingPostBody grading)
        {
            if (!Authenticate(token))
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            if (string.IsNullOrEmpty(grading.Email))
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new GradingLogsNotProvidedError());
                return response;
            }

            if (grading.Grading.Count == 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new GradingLogsNotProvidedError());
                return response;
            }

            if (grading.Grading.Count > 1000)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new ClientLogsExceededLimitError());
                return response;
            }

            var logs = new GradingAppLogUploadHelper().GetLogsFromMessageBody(grading);

            // Save the logs in the database
            var repo = new PaymentRepository();
            var indRepo = new IndividualRepository();
            var maxAdmissionGrade = Enum.GetValues(typeof(AdmissionGrade)).Cast<AdmissionGrade>().Max();

            if (authority.ToLower() == GradingAuthority.Panel.ToString().ToLower())
            {
                foreach (var log in logs)
                {
                    if(log.Grade >= 0 && log.Grade <= maxAdmissionGrade)
                    {
                        repo.SavePanelGrading(log);
                        var individual = indRepo.GetIndividualIdsByEmail(log.Email);
                        indRepo.UpdateIndividualPaymentVerifiedFlag(individual.Id, PaymentStatus.Verified.ToString());
                    }
                }
            }
            else if (authority.ToLower() == GradingAuthority.Ceo.ToString().ToLower())
            {
                foreach (var log in logs)
                {
                    if (log.Grade >= 0 && log.Grade <= maxAdmissionGrade)
                    {
                        repo.SaveCeoGrading(log);
                        var individual = indRepo.GetIndividualIdsByEmail(log.Email);
                        indRepo.UpdateIndividualPaymentVerifiedFlag(individual.Id, PaymentStatus.Verified.ToString());
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
