using System;
using System.Collections.Generic;

namespace Connect.Classes.DataModels
{
	public class LoggedInUser
	{
		public Guid Id { get; set; }
		public int IndividualId { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public UserType UserType { get; set; }
        public string Batch { get; set; }
        public IList<string> Sections { get; set; }
        public IList<string> Subject { get; set; }
        public string Section { get; set; }
    }

    public enum UserType
	{
		Applicant,
		Panel,
		PaymentAdministrator,
		Administrator,
		Ceo,
        Manager,
        QuizAdmin,
        AttendanceAdmin
	}
}