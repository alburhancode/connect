using System.ComponentModel.DataAnnotations;

namespace Connect.Models
{
	public class LoginViewModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		[DataType(DataType.Password, ErrorMessage = "between 6 and 12 characters are allowed")]
		public string Password { get; set; }
		public bool HasAuthenticationFailed { get; set; }
		public string ForgotPasswordUsername { get; set; }
		public string AuthenticationErrorMessage = "You have tried to login with incorrect username or password";
		public string InvalidEmailAddressErrorMessage = "This e-mail address does not exist in our records. Please re-enter";
	}
}