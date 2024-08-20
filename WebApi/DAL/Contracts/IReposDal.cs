using WebApi.Models.Models;

namespace WebApi.DAL.Contracts
{
	public interface IReposDal
	{
		Repo? GetRepoByFullName(string fullName);

		IEnumerable<Repo> GetReposByFullNames(List<string> fullNames);

		IEnumerable<Repo> GetReposByKeyword(string keyword);
	}
}
