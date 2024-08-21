namespace WebApi.Models.Models
{
	/// <summary>note: had we used bearer authentication and not cookie authentication,<para />
	/// we'd add a Token property to this model, to save in the client (LS etc) after successful login, so he'd be able to re-transmit it</summary>
	public class UserAuthentication
	{
		/// <summary></summary>
		public bool IsLoggedIn { get; set; } = false;

		/// <summary></summary>
		public string? Username { get; set; } = null;
	}
}
