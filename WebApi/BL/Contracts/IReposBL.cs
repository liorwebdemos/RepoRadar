using WebApi.Models;

namespace WebApi.BL.Contracts
{
	public interface IReposBL
	{
		/// <summary>get repos by keyword</summary>
		IEnumerable<Repo> GetReposByKeyword(string? keyword);
	}
}
