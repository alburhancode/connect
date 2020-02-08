using System;
using System.Collections.Generic;
using Connect.Classes;

namespace Connect.Models.Interview
{
	public class InterviewViewModel
	{
		public IEnumerable<Interviewee> Interviewees { get; set; }
	}

	public class Interviewee
	{
		public string Phone { get; set; }
		public string Email { get; set; }
		public string NicNumber { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string FatherName { get; set; }
		public string IndividualId { get; set; }
		public int Age { get; set; }
		public RegistrationStatus RegistrationStatus { get; set; }
		public DateTime Created { get; set; }
		public string InterviewDateAndTime { get; set; }
		public int CeoGrade;
		public int PanelGrade;
		public string CeoGradeValue => GetGradeValue(CeoGrade);
		public string PanelGradeValue => GetGradeValue(PanelGrade);
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