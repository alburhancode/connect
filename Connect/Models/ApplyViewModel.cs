using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Connect.Models
{
	public class ApplyViewModel
	{
		[Required]
		[MaxLength(50)]
		[RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Only alphabets and numbers are allowed")]
		public string Name { get; set; }
		[Required]
		[MaxLength(50)]
		[DataType(DataType.EmailAddress, ErrorMessage = "Enter in the format e.g username@gmail.com")]
		[Remote("DoesEmailExist", "Validation", HttpMethod = "GET", ErrorMessage = "Email Already Exists")]
		public string Email { get; set; }
		[Required]
		[RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed with a maximum of 14 digits.")]
		public long? Phone { get; set; }
		[Required]
		[RegularExpression(@"^(1[2-9]|[2-6][0-9]|99)$", ErrorMessage = "Your age must range between 12 and 99.")]
		public int? Age { get; set; }

		[Required]
		[DataType(DataType.Password, ErrorMessage = "between 6 and 12 characters are allowed")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password, ErrorMessage = "between 6 and 12 characters are allowed")]
		public string ConfirmPassword { get; set; }

		public string PaymentMethod { get; set; }

		[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a 10 digit number")]
		public long? PaymentCodeEasyPaisa { get; set; }

		[RegularExpression(@"^\d{4}$", ErrorMessage = "Please enter the numbers in the format [1234 - 5678]")]
		public int? PaymentCodeManualPreHyphen { get; set; }
		public int? PaymentCodeManualPostHyphen { get; set; }

		//public List<InterviewSlot> AvailableInterviewSlots { get; set; }
        //public List<BatchTiming> AvailableBatchTimings { get; set; }
        public int SelectedInterviewSlotId { get; set; }
        public int SelectedBatchTimingId { get; set; }

        //public bool PaidThroughJazz { get; set; }
        //public bool PaidCashAtInstitute { get; set; }
        //public bool PaidThroughOnlineBank { get; set; }
        //public string PaymentMethod { get; set; }
        //public string PaymentCodeJazz { get; set; }
        //public string PaymentCodeOnlineBank { get; set; }
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed with a maximum of 13 digits.")]
        //public string PaymentCodeManual { get; set; }
        public string Message { get; set; } = "";
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only alphabets and numbers are allowed. No spaces are allowed")]
        //public string FatherName { get; set; }
        //[Required]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only alphabets and numbers are allowed. No spaces are allowed")]
        //public string LastName { get; set; }

        //public ApplyingFor ApplyingFor { get; set; }
        //public CurrentSection CurrentSection { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public Campus Campus { get; set; }
    }

    public class ApplyAdminViewModel
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Only alphabets and numbers are allowed")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter in the format e.g username@gmail.com")]
        [Remote("DoesEmailExist", "Validation", HttpMethod = "GET", ErrorMessage = "Email Already Exists")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed with a maximum of 14 digits.")]
        public long? Phone { get; set; }
        [Required]
        [RegularExpression(@"^(1[2-9]|[2-6][0-9]|99)$", ErrorMessage = "Your age must range between 12 and 99.")]
        public int? Age { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "between 6 and 12 characters are allowed")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "between 6 and 12 characters are allowed")]
        public string ConfirmPassword { get; set; }

        public string PaymentMethod { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a 10 digit number")]
        public long? PaymentCodeEasyPaisa { get; set; }

        //[RegularExpression(@"^\d{4}$", ErrorMessage = "Please enter the numbers in the format [1234 - 5678]")]
        //public int? PaymentCodeManualPreHyphen { get; set; }
        //public int? PaymentCodeManualPostHyphen { get; set; }

        //public int SelectedBatchTimingId { get; set; }

        public string Message { get; set; } = "";
        public CurrentStatus CurrentStatus { get; set; }
        public Campus Campus { get; set; }
        public int SelectedBatchTimingId { get; set; }
    }
}