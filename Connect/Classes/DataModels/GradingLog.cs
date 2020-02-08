using Connect.Classes.Enums;

namespace Connect.Classes.DataModels
{
    public class GradingLog : IGradingLog
    {
        public GradingLog(string email, AdmissionGrade grade, string comments)
        {
            Email = email;
            Grade = grade;
            Comments = comments;
        }

        public string Email { get; set; }
        public AdmissionGrade Grade { get; set; }
        public string Comments { get; set; }
    }
}