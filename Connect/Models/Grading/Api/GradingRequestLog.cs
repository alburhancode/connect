using Connect.Classes.Enums;
using System;

namespace Connect.Models.Grading.Api
{
    public class GradingRequestLog
    {
        public int IndividualId { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }

        public AdmissionGrade Grade;

        public string Comments;
    }
}