using System;
using System.ComponentModel.DataAnnotations;
using Connect.Classes;

namespace Connect.Models.Interview
{
	[Serializable]
	public class PanelGradingViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int IndividualId { get; set; }

		public RegistrationStatus RegistrationStatus { get; set; }
		[DataType(DataType.MultilineText)]
		public string IntroProfileDescription { get; set; }
		[DataType(DataType.MultilineText)]
		public string IntroKeyAchievements { get; set; }
		[DataType(DataType.MultilineText)]
		public string MotivationReasonForEnrollment { get; set; }
		[DataType(DataType.MultilineText)]
		public string MotivationGoals { get; set; }
		[DataType(DataType.MultilineText)]
		public string MotivationIfNotSelected { get; set; }

		[DataType(DataType.MultilineText)]
		public string StrengthsTopTwo { get; set; }
		[DataType(DataType.MultilineText)]
		public string StrengthsHobbies { get; set; }
		[DataType(DataType.MultilineText)]
		public string CommittmentAnyCourseBefore { get; set; }
		[DataType(DataType.MultilineText)]
		public string CommittmentFulfillPromises { get; set; }
		[DataType(DataType.MultilineText)]
		public string CommittmentExample { get; set; }
		[DataType(DataType.MultilineText)]
		public string CommittmentFailures { get; set; }
		[DataType(DataType.MultilineText)]
		public string AdditionalSkillsAnyOther { get; set; }
		[DataType(DataType.MultilineText)]
		public string AdditionalSkillsSocial { get; set; }
		public int GradeCommitment { get; set; }
		public int GradeStrengths { get; set; }
		public int GradePassion { get; set; }
		public int GradeFunctionalExpertise { get; set; }
		public int GradeExistingAffiliation { get; set; }
		public int PanelGrade { get; set; }
		[DataType(DataType.MultilineText)]
		[Required]
		public string PanelComments { get; set; }
	}
}