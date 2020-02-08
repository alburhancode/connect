using System;
using System.ComponentModel.DataAnnotations;

namespace Connect.Classes.DataModels
{
    public class IndividualLeave
    {
        public Guid Id { get; set; }
        public int LeaveId { get; set; }
        public int IndividualId { get; set; }
        [Display(Name = "Date Of Leave")]
        public DateTime DateOfLeave { get; set; }
        public string Reason { get; set; }
    }
}