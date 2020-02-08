using System;
using System.Collections.Generic;
using Connect.Classes;

namespace Connect.Models.Payment
{
	public class PaymentViewModel
	{
		public IEnumerable<Payment> Payments { get; set; }
	}

	public class Payment
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string FatherName { get; set; }
		public string NicNumber { get; set; }
		public string Email { get; set; }
		public string IndividualId { get; set; }
		public string PaymentCode { get; set; }
		public string PaymentMethod { get; set; }
		public long Phone { get; set; }
		public string Password { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public CurrentStatus CurrentStatus { get; set; }
        public string CurrentStatusValue { get; set; }
        public int Age { get; set; }
		public RegistrationStatus RegistrationStatus { get; set; }
		public DateTime Created { get; set; }
		public string Slot { get; set; }
        public DateTime? DateLastReminded { get; set; }
        public string WhatsAppNumber { get; set; }
        public string Timing { get; set; }
    }
}