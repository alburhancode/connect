using Connect.Classes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Connect.Models.Payment
{
	public class PaymentDeclinedViewModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; } = "";
		public string PaymentMethod { get; set; }
		[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a 10 digit number")]
		public long? PaymentCodeEasyPaisa { get; set; }

		[RegularExpression(@"^\d{4}$", ErrorMessage = "Please enter the numbers in the format [1234 - 5678]")]
		public int? PaymentCodeManualPreHyphen { get; set; }
		public int? PaymentCodeManualPostHyphen { get; set; }
        public string PaymentCode { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}