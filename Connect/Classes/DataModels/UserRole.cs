using System.ComponentModel;

namespace Connect.Classes.DataModels
{
	public enum UserRole
	{
		[Description("None")]
		None = 0,
		[Description("Applicant")]
		Applicant = 1,
		[Description("Panel")]
		Panel = 2,
		[Description("Accountant")]
		Accountant = 3,
		[Description("Administrator")]
		Administrator = 4
	}
}