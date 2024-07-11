using System.ComponentModel.DataAnnotations;

namespace Posts.API.Models.DTOs
{
	public class RegisterRequestDto
	{
		[Required]
		public string Username { get; set; } = null!;
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		[RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])(?=^[^\s]*[@$!%*?&][^\s]*$)[A-Za-z\d@$!%*?&]{6,}$",
		ErrorMessage = "Password must contain at least one uppercase letter, exactly one special character, and be at least 8 characters long.")]
		public string Password { get; set; } = null!;

		[Required]
		public string Role { get; set; }
	}
}
