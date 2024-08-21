namespace WebApi.Models.Models
{
	/// <summary>GitHub repository<para />
	/// note: only part of the props of the full entity are provided in this model</summary>
	public class RepoRestDto
	{
		/// <summary></summary>
		public int Id { get; set; } = 0;

		/// <summary></summary>
		public string Name { get; set; } = string.Empty;

		/// <summary></summary>
		public string FullName { get; set; } = string.Empty;

		/// <summary></summary>
		public string HtmlUrl { get; set; } = string.Empty;

		/// <summary></summary>
		public string Description { get; set; } = string.Empty;

		/// <summary></summary>
		public DateTimeOffset? CreatedAt { get; set; }
	}
}
