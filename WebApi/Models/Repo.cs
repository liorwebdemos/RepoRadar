namespace WebApi.Models
{
	public class Repo
	{
		public int Id { get; set; } = 0;
		public string Name { get; set; } = String.Empty;
		public string FullName { get; set; } = String.Empty;
		public string HtmlUrl { get; set; } = String.Empty;
		public string Description { get; set; } = String.Empty;
		public DateTimeOffset? CreatedAt { get; set; }
		public string Language { get; set; } = String.Empty;
	}
}
