using Connect.Classes.DataModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Connect.Models.Quiz
{
    public class IndividualsBySectionViewModel
    {
        public List<IndividualBasics> IndividualBasics { get; set; }
        public List<SelectListItem> EventNames { get; set; }
    }

    public class IndividualAttendanceBySectionViewModel
    {
        public List<AttendanceFullSummary> AttendanceSummary { get; set; }
    }

    public class OverallAttendanceViewModel
    {
        public IList<OverallAttendance> OverallAttendance { get; set; }
    }

    public class OverallAttendance
    {
        public string Section { get; set; }
        public int Present { get; set; }
        public int Total { get; set; }
        public int Leave { get; set; }
        public int Percentile { get; set; }
        public int LeavePercentile { get; set; }
    }
}