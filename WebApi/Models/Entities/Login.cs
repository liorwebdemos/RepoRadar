using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
	public class Login
	{
		/// <summary></summary>
		[Required]
		public string Username { get; set; } = String.Empty;

		/// <summary></summary>
		[Required]
		public string Password { get; set; } = String.Empty;
	}
}
