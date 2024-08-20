using WebApi.Models.Models;

namespace WebApi.BL.Contracts
{
	public interface IReposBL
	{
		IEnumerable<ExtendedRepo> GetExtendedReposByKeyword(string? keyword);

		IEnumerable<ExtendedRepo> GetExtendedReposByFullNames(List<string> fullNames);

		IEnumerable<ExtendedRepo> GetFavoriteExtendedRepos();

		ExtendedRepo SetFavoriteByFullName(string fullName);

		ExtendedRepo SetUnfavoriteByFullName(string fullName);
	}
}
