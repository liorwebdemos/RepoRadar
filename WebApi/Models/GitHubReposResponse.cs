namespace WebApi.Models
{
	public class GitHubReposResponse
	{
		/// <summary>search results count</summary>
		public int TotalCount { get; set; }

		/// <summary>repos</summary>
		public IEnumerable<Repo> Items { get; set; } = Enumerable.Empty<Repo>();
	}
}
