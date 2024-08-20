using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Models
{
	/// <summary>GitHub repository<para />
	/// note: only part of the props of the full entity are provided in this model</summary>
	public class Repo
	{
		/// <summary></summary>
		public string Name { get; set; } = string.Empty;

		/// <summary></summary>
		[RegularExpression(@"^[a-zA-Z0-9]+/[a-zA-Z0-9]+$", ErrorMessage = "The value must be in the format: letters and/or numbers, followed by a '/', followed by more letters and/or numbers.")]
		public string FullName { get; set; } = string.Empty;

		/// <summary></summary>
		public string Url { get; set; } = string.Empty;

		/// <summary></summary>
		public string Description { get; set; } = string.Empty;

		/// <summary></summary>
		public DateTimeOffset? CreatedAt { get; set; }
	}
}
