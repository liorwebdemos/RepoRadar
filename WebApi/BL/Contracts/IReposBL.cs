using WebApi.Models;

namespace WebApi.BL.Contracts
{
	public interface IReposBL
	{
		IEnumerable<Repo> GetReposByKeyword(string keyword);
	}
}
