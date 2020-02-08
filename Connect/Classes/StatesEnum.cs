using System.ComponentModel.DataAnnotations;

namespace Connect.Classes
{
	//public enum StatesEnum
	//{
	//	//LockedForPaymentVerification,
	//	//PaymentVerificationFailed,
	//	StepOneCompleted,
	//	StepTwoCompleted,
	//	StepThreeCompleted,
	//	//IdentityDataComplete,
	//	//FormCompleted,
	//	//FormLocked,
	//	Enrolled
	//}

	public enum RegistrationStatus
	{
		UnRegistered,
		Registered,
		Completed,
		Locked,
		PanelInterviewed,
		CeoInterviewed,
		//Graded,
		Enrolled,
		Rejected,
		Blocked,
        Dropped
	}

	public enum PaymentStatus
	{
        [Display(Name = "UnVerified")]
        UnVerified,
        [Display(Name = "Verified")]
        Verified,
        [Display(Name = "Declined")]
        Declined,
        [Display(Name = "Blocked")]
        Blocked
    }

    public enum AdvancedSelection
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "Advanced Course")]
        AdvancedCourse,
        [Display(Name = "Quran Classes")]
        QuranClasses,
        [Display(Name = "Dars e Nizami")]
        DarseNizami
    }
}