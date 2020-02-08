using Connect.Classes.Dapper;
using Connect.Models.Attendance.App;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Connect.Controllers
{
    [RoutePrefix("api/v1/attendance")]
    public class AttendanceController : ApiController
    {
        [HttpGet]
        [Route("{batch}/{section}")]
        public IHttpActionResult Get(string batch, string section)
        {
            var attRepository = new AttendanceRepository();
            var individuals = attRepository.GetIndividualsBySectionForAttendance(batch, section, 0);
            // for each individual, add its history
            foreach (var individual in individuals)
            {
                individual.AttendanceHistory = attRepository.GetAttendanceHistory(individual.RollNumber);
            }

            return Ok(individuals);
        }

        private bool Authenticate(string token)
        {
            return token == ConfigurationManager.AppSettings["AttendanceAuthenticationToken"];
        }

        [HttpPost]
        [Route("save/{token}")]
        public HttpResponseMessage Create(string token, [FromBody]AttendancePostBody attendance)
        {
            if (!Authenticate(token))
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            if (attendance?.Attendance == null || attendance.Attendance.Count == 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new AttendanceLogsNotProvidedError());
                return response;
            }

            if (attendance.Attendance.Count > 1000)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new ClientLogsExceededLimitError());
                return response;
            }

            var logs = new AttendanceAppLogUploadHelper().GetLogsFromMessageBody(attendance);

            // Save the logs in the database
            var attRepository = new AttendanceRepository();
            foreach (var att in logs)
            {
                attRepository.SaveAttendance(att);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
