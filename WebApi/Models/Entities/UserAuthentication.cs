namespace WebApi.Models.Models
{
	/// <summary>a wrapper for the JWT token</summary>
	public class UserAuthentication
	{
		/// <summary></summary>
		public string Token { get; set; } = string.Empty;

		/// <summary></summary>
		public DateTimeOffset ExpiryDate { get; set; } = DateTimeOffset.MinValue;
	}
}
