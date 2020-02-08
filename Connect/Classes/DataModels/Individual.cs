using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Connect.Classes.Enums;
using Connect.Models;

namespace Connect.Classes.DataModels
{
	public class Individual
	{
        public string Section { get; set; }
        public AdvancedSelection AdvancedSelection { get; set; }
        public IEnumerable<Note> Notes { get; set; }
        public int IndividualId { get; set; }
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public long? Phone { get; set; }
		public string Password { get; set; }
		public UserType UserType { get; set; }
		public int? Age { get; set; }
		public RegistrationStatus RegistrationStatus { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
        [Display(Name = "Payment Code")]
        public string PaymentCode { get; set; }
        [Display(Name = "NIC Number")]
        public long? NicNumber { get; set; }
        [Display(Name = "Other Contact")]
        public long? OtherContact { get; set; }
        [Display(Name = "Marital Status")]
        public MaritalStatus MaritalStatus { get; set; }

        [Display(Name = "Residential Address")]
        public string ResidentialAddress { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Matric GPA")]
        public int? MatricGpa { get; set; }

        [Display(Name = "Matric Subjects")]
        public string MatricSubjects { get; set; }

        [Display(Name = "Matric Institute")]
        public string MatricInstitute { get; set; }

        [Display(Name = "Intermediate GPA")]
        public int? IntermediateGpa { get; set; }

        [Display(Name = "Intermediate Subjects")]
        public string IntermediateSubjects { get; set; }

        [Display(Name = "Intermediate Institute")]
        public string IntermediateInstitute { get; set; }

        [Display(Name = "Graduation GPA")]
        public int? GraduationGpa { get; set; }

        [Display(Name = "Graduation Subjects")]
        public string GraduationSubjects { get; set; }

        [Display(Name = "Graduation Institute")]
        public string GraduationInstitute { get; set; }

        [Display(Name = "Masters GPA")]
        public int? MastersGpa { get; set; }

        [Display(Name = "Masters Subjects")]
        public string MastersSubjects { get; set; }

        [Display(Name = "Masters Institution")]
        public string MastersInstitution { get; set; }

        // Ms
        [Display(Name = "MS GPA")]
        public int? MsGpa { get; set; }

        [Display(Name = "MS Subjects")]
        public string MsSubjects { get; set; }

        [Display(Name = "MS Institution")]
        public string MsInstitution { get; set; }

        [Display(Name = "Phd Subjects")]
        public string PhdSubjects { get; set; }

        [Display(Name = "Phd Institution")]
        public string PhdInstitution { get; set; }

        [Display(Name = "Other Qualification")]
        public string OtherQualification { get; set; }


        [Display(Name = "Current Job")]
        public string CurrentJob { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Display(Name = "Total JobExperience")]
        public string TotalJobExperience { get; set; }

        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Display(Name = "Business Area")]
        public string BusinessArea { get; set; }

        [Display(Name = "Business Nature")]
        public string BusinessNature { get; set; }

        [Display(Name = "Applied Before")]
        public bool AppliedBefore { get; set; }

        [Display(Name = "Any Religious Education")]
        public bool AnyReligiousEducation { get; set; }

        [Display(Name = "Religious Education Specify")]
        public string ReligiousEducationSpecify { get; set; }

        [Display(Name = "Any Other Expertise")]
        public string AnyOtherExpertise { get; set; }
        [Display(Name = "Current Status")]
        public CurrentStatus CurrentStatus { get; set; }
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }
        [Display(Name = "Education")]
        public Education Education { get; set; }

        public int? InterviewSlotId { get; set; }
        public int? BatchTimingId { get; set; }

        public int CeoGrade;
        public int PanelGrade;
        public string CeoGradeValue => GetGradeValue(CeoGrade);
        public string PanelGradeValue => GetGradeValue(PanelGrade);
        public string Batch { get; set; }

        // new fields added for sisters
        [Display(Name = "Father Or Husband Name")]
        public string FatherOrHusbandName { get; set; }
        [Display(Name = "Class Selection")]
        public string ClassSelection { get; set; }
        [Display(Name = "WhatsApp Number")]
        public string WhatsAppNumber { get; set; }
        [Display(Name = "Education Details")]
        public string EducationDetails { get; set; }
        [Display(Name = "Responsibilities")]
        public string Responsibilities { get; set; }
        [Display(Name = "Profession/Study/Business Details")]
        public string ProfessionStudyBusinessDetails { get; set; }
        [Display(Name = "City Details")]
        public string CityDetails { get; set; }
        public IList<string> Sections { get; set; }
        public IList<string> Subject { get; set; }
        public Campus Campus { get; set; }

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

    public class IndividualGet
    {
        public int IndividualId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }

        [Display(Name = "Residential Address")]
        public string ResidentialAddress { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Matric GPA")]
        public int? MatricGpa { get; set; }

        [Display(Name = "Matric Subjects")]
        public string MatricSubjects { get; set; }

        [Display(Name = "Matric Institute")]
        public string MatricInstitute { get; set; }

        [Display(Name = "Intermediate GPA")]
        public int? IntermediateGpa { get; set; }

        [Display(Name = "Intermediate Subjects")]
        public string IntermediateSubjects { get; set; }

        [Display(Name = "Intermediate Institute")]
        public string IntermediateInstitute { get; set; }

        [Display(Name = "Graduation GPA")]
        public int? GraduationGpa { get; set; }

        [Display(Name = "Graduation Subjects")]
        public string GraduationSubjects { get; set; }

        [Display(Name = "Graduation Institute")]
        public string GraduationInstitute { get; set; }

        [Display(Name = "Masters GPA")]
        public int? MastersGpa { get; set; }

        [Display(Name = "Masters Subjects")]
        public string MastersSubjects { get; set; }

        [Display(Name = "Masters Institution")]
        public string MastersInstitution { get; set; }

        //// Ms
        [Display(Name = "MS GPA")]
        public int? MsGpa { get; set; }

        [Display(Name = "MS Subjects")]
        public string MsSubjects { get; set; }

        [Display(Name = "MS Institution")]
        public string MsInstitution { get; set; }

        [Display(Name = "Phd Subjects")]
        public string PhdSubjects { get; set; }

        [Display(Name = "Phd Institution")]
        public string PhdInstitution { get; set; }

        [Display(Name = "Other Qualification")]
        public string OtherQualification { get; set; }


        [Display(Name = "Current Job")]
        public string CurrentJob { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Display(Name = "Total JobExperience")]
        public string TotalJobExperience { get; set; }

        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Display(Name = "Business Area")]
        public string BusinessArea { get; set; }

        [Display(Name = "Business Nature")]
        public string BusinessNature { get; set; }

        public AdmissionGrade PanelGrade;
        public string PanelGradeString
        {
            get
            {
                return PanelGrade.ToString();
            }
        }

        public string PanelComments { get; set; }
    }

    public class IndividualByCnic
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long? Phone { get; set; }
        public int? Age { get; set; }

        public string RegistrationStatusValue => RegistrationStatus.ToString();
        [JsonIgnore]
        public RegistrationStatus RegistrationStatus { get; set; }

        public string PaymentStatusValue => PaymentStatus.ToString();
        [JsonIgnore]
        public PaymentStatus PaymentStatus { get; set; }

        //[Display(Name = "Payment Code")]
        //public string PaymentCode { get; set; }
        [Display(Name = "NIC Number")]
        public long? NicNumber { get; set; }
        [Display(Name = "Other Contact")]
        public long? OtherContact { get; set; }
        [Display(Name = "Marital Status")]
        public MaritalStatus MaritalStatus { get; set; }

        [Display(Name = "Residential Address")]
        public string ResidentialAddress { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Matric GPA")]
        public int? MatricGpa { get; set; }

        [Display(Name = "Matric Subjects")]
        public string MatricSubjects { get; set; }

        [Display(Name = "Matric Institute")]
        public string MatricInstitute { get; set; }

        [Display(Name = "Intermediate GPA")]
        public int? IntermediateGpa { get; set; }

        [Display(Name = "Intermediate Subjects")]
        public string IntermediateSubjects { get; set; }

        [Display(Name = "Intermediate Institute")]
        public string IntermediateInstitute { get; set; }

        [Display(Name = "Graduation GPA")]
        public int? GraduationGpa { get; set; }

        [Display(Name = "Graduation Subjects")]
        public string GraduationSubjects { get; set; }

        [Display(Name = "Graduation Institute")]
        public string GraduationInstitute { get; set; }

        [Display(Name = "Masters GPA")]
        public int? MastersGpa { get; set; }

        [Display(Name = "Masters Subjects")]
        public string MastersSubjects { get; set; }

        [Display(Name = "Masters Institution")]
        public string MastersInstitution { get; set; }

        // Ms
        [Display(Name = "MS GPA")]
        public int? MsGpa { get; set; }

        [Display(Name = "MS Subjects")]
        public string MsSubjects { get; set; }

        [Display(Name = "MS Institution")]
        public string MsInstitution { get; set; }

        [Display(Name = "Phd Subjects")]
        public string PhdSubjects { get; set; }

        [Display(Name = "Phd Institution")]
        public string PhdInstitution { get; set; }

        [Display(Name = "Other Qualification")]
        public string OtherQualification { get; set; }


        [Display(Name = "Current Job")]
        public string CurrentJob { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Display(Name = "Total JobExperience")]
        public string TotalJobExperience { get; set; }

        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Display(Name = "Business Area")]
        public string BusinessArea { get; set; }

        [Display(Name = "Business Nature")]
        public string BusinessNature { get; set; }

        [Display(Name = "Applied Before")]
        public bool AppliedBefore { get; set; }

        [Display(Name = "Any Religious Education")]
        public bool AnyReligiousEducation { get; set; }

        [Display(Name = "Religious Education Specify")]
        public string ReligiousEducationSpecify { get; set; }

        [Display(Name = "Any Other Expertise")]
        public string AnyOtherExpertise { get; set; }

        public string CurrentStatusValue => CurrentStatus.ToString();
        [JsonIgnore]
        public CurrentStatus CurrentStatus { get; set; }
        //[Display(Name = "Payment Method")]
        //public string PaymentMethod { get; set; }

        public string EducationValue => Education.ToString();
        [JsonIgnore]
        public Education Education { get; set; }

        //public int? InterviewSlotId { get; set; }
        //public int? BatchTimingId { get; set; }

        [JsonIgnore]
        public int CeoGrade;
        [JsonIgnore]
        public int PanelGrade;

        public string CeoGradeValue => GetGradeValue(CeoGrade);
        public string PanelGradeValue => GetGradeValue(PanelGrade);

        public string CeoComments { get; set; }

        public string Batch { get; set; }

        public string Section { get; set; }

        // new fields added for sisters
        [Display(Name = "Father Or Husband Name")]
        public string FatherOrHusbandName { get; set; }
        [Display(Name = "Class Selection")]
        public string ClassSelection { get; set; }
        [Display(Name = "WhatsApp Number")]
        public string WhatsAppNumber { get; set; }
        //[Display(Name = "Education Details")]
        //public string EducationDetails { get; set; }
        //[Display(Name = "Responsibilities")]
        //public string Responsibilities { get; set; }
        //[Display(Name = "Profession/Study/Business Details")]
        //public string ProfessionStudyBusinessDetails { get; set; }
        //[Display(Name = "City Details")]
        //public string CityDetails { get; set; }

        public string CampusValue => Campus.ToString();
        [JsonIgnore]
        public Campus Campus { get; set; }
//        public Campus Campus { get; set; }

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