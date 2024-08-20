namespace WebApi.Models.Models
{
	/// <summary>GitHub repository<para />
	/// note: only part of the props of the full entity are provided in this model</summary>
	public class RepoGraphQlDto
	{
		/// <summary></summary>
		public string Name { get; set; } = string.Empty;

		/// <summary></summary>
		public RepoOwnerInGraphQlDto Owner { get; set; } = new RepoOwnerInGraphQlDto();

		/// <summary></summary>
		public string Url { get; set; } = string.Empty;

		/// <summary></summary>
		public string Description { get; set; } = string.Empty;

		/// <summary></summary>
		public DateTimeOffset? CreatedAt { get; set; }
	}

	public class RepoOwnerInGraphQlDto
	{
		public string Login { get; set; } = string.Empty;
	}
}
