using Connect.Classes;
using System;
using System.Collections.Generic;

namespace Connect.Models.Admissions
{
    public class AdmissionViewModel
    {
        public IEnumerable<Admission> Admissions { get; set; }
    }

    public class Admission
    {
        public string RollNumberFormat { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string IndividualId { get; set; }
        public long Phone { get; set; }
        public int Age { get; set; }
        public RegistrationStatus RegistrationStatus { get; set; }
        public DateTime Created { get; set; }
        public string CeoComments { get; set; }
        public int CeoGrade;
        public int PanelGrade;
        public string CeoGradeValue => GetGradeValue(CeoGrade);
        public string PanelGradeValue => GetGradeValue(PanelGrade);
        public bool EmailSent { get; set; }
        public string Section { get; set; }
        public int RollNumber { get; set; }
        public string Timing { get; set; }

        private string GetGradeValue(int grade)
        {
            switch (grade)
            {
                case 1:
                    return "C";
                case 2:
                    return "B";
                case 3:
                    return "B+";
                case 4:
                    return "A-";
                case 5:
                    return "A";
                case 6:
                    return "A+";
                case 7:
                    return "Listener";
                case 8:
                    return "Not Graded";
                default:
                    return "";
            }
        }
    }
}