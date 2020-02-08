using System;

namespace Connect.Classes.DataModels
{
    public class IndividualAttendance
    {
        public Guid Id { get; set; }
        public int AttendanceId { get; set; }
        public string AttendanceTakenOn { get; set; }
        public DateTime AttendanceUpdatedOn { get; set; }
        public string Subject { get; set; }
        public int Status { get; set; }
    }
}