using Connect.Classes.DataModels;
using Connect.Models.Grading.Api;
using System.Collections.Generic;
using System.Linq;

namespace Connect.Controllers
{
    public class GradingAppLogUploadHelper
    {
        public IList<IGradingLog> GetLogsFromMessageBody(GradingPostBody messageBody)
        {
            var logs = new List<IGradingLog>();

            if (messageBody?.Grading == null)
            {
                return logs;
            }

            foreach (var l in messageBody.Grading)
            {
                logs.Add(new GradingLog(messageBody.Email, l.Grade, l.Comments));
            }

            return logs.ToList();
        }

    }
}