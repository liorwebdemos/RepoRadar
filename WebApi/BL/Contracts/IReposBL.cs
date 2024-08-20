using WebApi.Models.Models;

namespace WebApi.BL.Contracts
{
	public interface IReposBL
	{
		IEnumerable<Repo> GetReposByKeyword(string? keyword);

		IEnumerable<Repo> GetReposByFullNames(List<string> fullNames);
	}
}
