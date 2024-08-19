using WebApi.Models;

namespace WebApi.DAL.Contracts
{
	public interface IReposRepo
	{
		IEnumerable<Repo> GetReposByKeyword(string keyword);
	}
}
