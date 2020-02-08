using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using Connect.Classes;
using Connect.Classes.DataModels;
using Connect.Classes.Enums;

namespace Connect.Models
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class AttachmentAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value,
			ValidationContext validationContext)
		{
			HttpPostedFileBase file = value as HttpPostedFileBase;

			// The file is required.
			//if (file == null)
			//{
			//	return new ValidationResult("Please upload a file!");
			//}
			if (file == null) return ValidationResult.Success;

			// The meximum allowed file size is 1MB.
			if (file.ContentLength > 1024 * 1024)
			{
				return new ValidationResult("The maximum allowed file size allowed is 1MB");
			}

			// Only PDF can be uploaded.
			string ext = Path.GetExtension(file.FileName);
			if (String.IsNullOrEmpty(ext) ||
			    !ext.Equals(".png", StringComparison.OrdinalIgnoreCase) &&
			    !ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) &&
			    !ext.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
			{
				return new ValidationResult("The file types allowed are PNG, JPG and JPEG");
			}
			// Everything OK.
			return ValidationResult.Success;
		}
	}

	public enum Education
	{
		Matric,
		Intermediate,
		Graduation,
		Masters,
		Ms,
		Phd
	}

	public enum CurrentStatus
	{
		Professional,
		Businessman,
		Student,
        Household,
        UnEmployed
	}

    public enum Campus
    {
        [Display(Name = "-- Please select --")]
        NONE,
        [Display(Name = "Karachi")]
        KHI,
        [Display(Name = "Islamabad (Men)")]
        ISB,
        [Display(Name = "Islamabad (Women)")]
        ISBW,
        [Display(Name = "Lahore")]
        LHR
    }

    //public enum CampusAdmin
    //{
    //    [Display(Name = "-- Please select --")]
    //    NONE,
    //    [Display(Name = "Islamabad (Men)")]
    //    ISB,
    //    [Display(Name = "Islamabad (Women)")]
    //    ISBW,
    //    [Display(Name = "Lahore")]
    //    LHR,
    //    [Display(Name = "Karachi")]
    //    KHI
    //}

    public enum Batch
    {
        [Display(Name = "December 2019 Islamabad (men)")]
        DEC2019ISB,
        [Display(Name = "December 2019 Islamabad (women)")]
        DEC2019ISBW,
        [Display(Name = "December 2019 Karachi (men)")]
        DEC2019KHI,
        [Display(Name = "December 2019 Lahore (men)")]
        DEC2019LHR
    }

    public class SignupViewModel
	{
        public AdvancedSelection AdvancedSelection { get; set; }
        public List<BatchTiming> AvailableTimings { get; set; }
        public List<InterviewSlot> AvailableInterviewSlots { get; set; }
		public int SelectedInterviewSlotId { get; set; }
        public int SelectedBatchTimingId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
//		public bool PaymentVerified { get; set; }
		public string PaymentMethod { get; set; }

		[RegularExpression(@"^\d{3}$", ErrorMessage = "Please enter the numbers in the format [123 - 4567]")]
		public int? PaymentCodeManualPreHyphen { get; set; }
		[RegularExpression(@"^\d{4}$", ErrorMessage = "Please enter the numbers in the format [123 - 4567]")]
		public int? PaymentCodeManualPostHyphen { get; set; }

		[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a 10 digit number")]
		public long? PaymentCodeEasyPaisa { get; set; }

		public CurrentStatus CurrentStatus { get; set; }
		public Education Education { get; set; }
		[Attachment]
		public HttpPostedFileBase Avatar { get; set; }
		[Attachment]
		public HttpPostedFileBase Cnic { get; set; }
		public Guid Id { get; set; }

		public string IndividualId { get; set; }
		public string Message { get; set; }
		public string Name { get; set; }
		public int? Age { get; set; }

		public long? Phone { get; set; }

		[Required]
		[RegularExpression(@"^[0-9]{13}$", ErrorMessage = "Please enter a 13 digit number")]
		public long? NicNumber { get; set; }

		[RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed with a maximum of 14 digits.")]
		public long? OtherContact { get; set; }

		[Required]
		public MaritalStatus MaritalStatus { get; set; }

		public string Email { get; set; }

		[Required]
		[MaxLength(128)]
		//[RegularExpression(@"^[A-Za-z0-9,/# ]*$", ErrorMessage = "Only numbers, aphabets and [,], [/], [#] are allowed.")]
		public string ResidentialAddress { get; set; }

		[Required]
		[MaxLength(128)]
		//[RegularExpression(@"^[A-Za-z0-9,/# ]*$", ErrorMessage = "Only numbers, aphabets and [,], [/], [#] are allowed.")]
		public string PermanentAddress { get; set; }

		#region Educational Background

		// matric
		[RegularExpression(@"^([0-9]|[1-8][0-9]|9[0-9]|100)$", ErrorMessage = "Only numbers are allowed within the range 0 and 100")]
		public int? MatricGpa { get; set; }

		[MaxLength(64)]
		public string MatricSubjects { get; set; }

		[MaxLength(64)]
		public string MatricInstitution { get; set; }

		// intermediate
		[RegularExpression(@"^([0-9]|[1-8][0-9]|9[0-9]|100)$", ErrorMessage = "Only numbers are allowed within the range 0 and 100")]
		public int? IntermediateGpa { get; set; }

		[MaxLength(64)]
		public string IntermediateSubjects { get; set; }

		[MaxLength(64)]
		public string IntermediateInstitution { get; set; }

		// Graduation
		[RegularExpression(@"^([0-9]|[1-8][0-9]|9[0-9]|100)$", ErrorMessage = "Only numbers are allowed within the range 0 and 100")]
		public int? GraduationGpa { get; set; }

		[MaxLength(64)]
		public string GraduationSubjects { get; set; }

		[MaxLength(64)]
		public string GraduationInstitution { get; set; }

		// Masters
		[RegularExpression(@"^([0-9]|[1-8][0-9]|9[0-9]|100)$", ErrorMessage = "Only numbers are allowed within the range 0 and 100")]
		public int? MastersGpa { get; set; }

		[MaxLength(64)]
		public string MastersSubjects { get; set; }

		[MaxLength(64)]
		public string MastersInstitution { get; set; }

		// Ms
		[RegularExpression(@"^([0-9]|[1-8][0-9]|9[0-9]|100)$", ErrorMessage = "Only numbers are allowed within the range 0 and 100")]
		public int? MsGpa { get; set; }

		[MaxLength(64)]
		public string MsSubjects { get; set; }

		[MaxLength(64)]
		public string MsInstitution { get; set; }

		[MaxLength(128)]
		public string PhdSubjects { get; set; }

		[MaxLength(128)]
		public string PhdInstitution { get; set; }

		[MaxLength(128)]
		public string OtherQualification { get; set; }

		#endregion

		#region Professional Information

		[MaxLength(64)]
		public string CurrentJob { get; set; }
		[MaxLength(64)]
		public string CompanyName { get; set; }
		[MaxLength(128)]
		public string Designation { get; set; }
		[MaxLength(64)]
		public string TotalJobExperience { get; set; }

		#endregion

		#region Business Information

		[MaxLength(64)]
		public string BusinessName { get; set; }

		[MaxLength(64)]
		public string BusinessArea { get; set; }

		[MaxLength(64)]
		public string BusinessNature { get; set; }

		#endregion

		#region Other Information

		public bool AppliedBefore { get; set; }

		public bool AnyReligiousEducation { get; set; }

		[MaxLength(128)]
		public string ReligiousEducationSpecify { get; set; }

		[MaxLength(128)]
		public string AnyOtherExpertise { get; set; }
		public string LastName { get; set; }
		public UserType UserType { get; set; }

		//public Grade PanelGrade { get; set; }
		//public Grade CeoGrade { get; set; }
		[MaxLength(512)]
		[DataType(DataType.MultilineText)]
		public string PanelComments { get; set; }
		[MaxLength(512)]
		[DataType(DataType.MultilineText)]
		public string CeoComments { get; set; }

        public RegistrationStatus RegistrationStatus { get; set; }

        // new fields added for sisters
        public string FatherOrHusbandName { get; set; }
        public string ClassSelection { get; set; }
        [Required]
        public string WhatsAppNumber { get; set; }
        public string EducationDetails { get; set; }
        public string Responsibilities { get; set; }
        public string ProfessionStudyBusinessDetails { get; set; }
        public string CityDetails { get; set; }

        #endregion
    }
}