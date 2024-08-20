using WebApi.Models.Models;

namespace WebApi.Models.DTOs
{
	/// <summary>GitHub repositories object - as received from the official GitHub REST api</summary>
	public class ReposRestDto
	{
		/// <summary>complete count of search results matching a search criteria.<para />
		/// note: can be bigger than the number of items in the repos array.</summary>
		public int TotalCount { get; set; }

		/// <summary>repos</summary>
		public IEnumerable<RepoRestDto> Items { get; set; } = Enumerable.Empty<RepoRestDto>();
	}
}
