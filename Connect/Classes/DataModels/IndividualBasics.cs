using System;
using System.Collections.Generic;

namespace Connect.Classes.DataModels
{
	public class IndividualBasics
	{
        public int IndividualId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
        public string RollNumber { get; set; }
        public int? MarksObtained { get; set; }
        public int MaxMarks { get; set; }
    }

    public class IndividualBasicsForAttendance
    {
        public int IndividualId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RollNumber { get; set; }
        public IList<AttendanceHistory> AttendanceHistory { get; set; }
    }

    public class AttendanceHistory
    {
        public string Subject { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool HasChangedSection { get; set; }
    }
}