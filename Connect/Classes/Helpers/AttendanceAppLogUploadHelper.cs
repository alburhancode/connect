using Connect.Classes.DataModels;
using Connect.Models.Attendance.App;
using System.Collections.Generic;
using System.Linq;

namespace Connect.Controllers
{
    public class AttendanceAppLogUploadHelper
    {
        public IList<IAttendanceLog> GetLogsFromMessageBody(AttendancePostBody messageBody)
        {
            var logs = new List<IAttendanceLog>();

            if (messageBody?.Attendance == null)
            {
                return logs;
            }

            foreach (var l in messageBody.Attendance)
            {
                logs.Add(new AttendanceLog(messageBody.Course, messageBody.Batch, messageBody.Section, messageBody.Semester, messageBody.Subject, messageBody.Email, messageBody.CreatedAt, l.RollNo, l.Status));
            }

            return logs.ToList();
        }

    }
}