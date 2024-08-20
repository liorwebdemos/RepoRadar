using Microsoft.IdentityModel.Tokens;
using WebApi.BL.Contracts;
using WebApi.DAL.Contracts;
using WebApi.Models.Models;

namespace WebApi.BL.Implementations
{
	public class ReposBL : IReposBL
	{
		private readonly IReposDal ReposRepo;

		public ReposBL(IReposDal reposRepo)
		{
			ReposRepo = reposRepo;
		}

		public IEnumerable<Repo> GetReposByKeyword(string? keyword)
		{
			if (string.IsNullOrWhiteSpace(keyword))
			{
				return Enumerable.Empty<Repo>();
			}
			return ReposRepo.GetReposByKeyword(keyword)
				.OrderBy(r => r.Name)
				.ToList(); // TODO: explain reasoning behind tolisting here
		}

		public IEnumerable<Repo> GetReposByFullNames(List<string> fullNames)
		{
			if (fullNames.IsNullOrEmpty())
			{
				return Enumerable.Empty<Repo>();
			}
			return ReposRepo.GetReposByFullNames(fullNames)
				.OrderBy(r => r.Name)
				.ToList();
		}
	}
}
