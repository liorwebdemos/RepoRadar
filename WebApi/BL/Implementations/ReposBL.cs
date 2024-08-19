using WebApi.BL.Contracts;
using WebApi.DAL.Contracts;
using WebApi.Models;

namespace WebApi.BL.Implementations
{
	public class ReposBL : IReposBL
	{
		private readonly IReposRepo ReposRepo;

		public ReposBL(IReposRepo reposRepo)
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
				.ToList(); // TODO: explain tolisting here
		}
	}
}
