using System.Collections.Generic;

namespace Connect.Models.Attendance.App
{
    public class AttendancePostBody
    {
        public string Course { get; set; }
        public string Batch { get; set; }
        public string Section { get; set; }
        public string Semester { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        
        public List<AttendanceRequestLog> Attendance { get; set; }

        public AttendancePostBody()
        {
            Attendance = new List<AttendanceRequestLog>();
        }
    }
}