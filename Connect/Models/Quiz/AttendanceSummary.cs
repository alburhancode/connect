using Connect.Classes.DataModels;
using System;
using System.Collections.Generic;

namespace Connect.Models.Quiz
{
    public class AttendanceSummary
    {
        public Guid Id { get; set; }
        public string RollNumber { get; set; }
        public string Name { get; set; }
        public int PresentCount { get; set; }
        public string Present { get; set; }
        public int AbsentCount { get; set; }
        public string Absent { get; set; }
        public int LeaveCount { get; set; }
        public string Leave { get; set; }
        public int TotalCount { get; set; }
    }

    public class AttendanceFullSummary
    {
        public Guid Id { get; set; }
        public int IndividualId { get; set; }
        public string RollNumber { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public List<AttendanceHistory> History { get; set; }
        //public CallOutcome LastCallOutcome { get; set; }
        public CallOutcome CallOutcome { get; set; }
        public string CallNotes { get; set; }
        public string Caller { get; set; }
        public CallerKefiyat Kefiyat { get; set; }
        //public List<string> AvailableCallers { get; set; }
    }
}