using Microsoft.IdentityModel.Tokens;
using WebApi.BL.Contracts;
using WebApi.DAL.Contracts;
using WebApi.Models.Models;

namespace WebApi.BL.Implementations
{
	public class ReposBL : IReposBL
	{
		private readonly IReposDal ReposDal;

		public ReposBL(IReposDal reposDal)
		{
			ReposDal = reposDal;
		}

		public IEnumerable<Repo> GetReposByKeyword(string? keyword)
		{
			if (string.IsNullOrWhiteSpace(keyword))
			{
				return Enumerable.Empty<Repo>();
			}
			return ReposDal.GetReposByKeyword(keyword)
				.OrderBy(r => r.Name)
				.ToList(); // note: can explain reasoning behind ToListing both here and in many other places
		}

		public IEnumerable<Repo> GetReposByFullNames(List<string> fullNames)
		{
			if (fullNames.IsNullOrEmpty())
			{
				return Enumerable.Empty<Repo>();
			}
			return ReposDal.GetReposByFullNames(fullNames)
				.OrderBy(r => r.Name)
				.ToList();
		}
	}
}
