namespace Connect.Classes.DataModels
{
    public class AttendanceLog : IAttendanceLog
    {
        public AttendanceLog(string course, string batch, string section, string semester, string subject, string email, string createdAt, string rollNumber, string status)
        {
            Course = course;
            Batch = batch;
            Section = section;
            Semester = semester;
            Subject = subject;
            Email = email;
            CreatedAt = createdAt;
            RollNumber = rollNumber;
            Status = status;
        }

        public string Course { get; set; }
        public string Batch { get; set; }
        public string Section { get; set; }
        public string Semester { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public string RollNumber { get; set; }
        public string Status { get; set; }
    }
}