using WebApi.Models.Models;

namespace WebApi.Models.DTOs
{
	/// <summary>GitHub repositories object - as received from the official GitHub GraphQl api</summary>
	public class ReposGraphQlDto
	{
		/// <summary>repos (in a dictionary format)</summary>
		public Dictionary<string, RepoGraphQlDto> Data { get; set; } = new Dictionary<string, RepoGraphQlDto>();
	}
}
