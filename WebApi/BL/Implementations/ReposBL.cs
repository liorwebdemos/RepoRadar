using WebApi.BL.Contracts;
using WebApi.DAL.Contracts;
using WebApi.Models;

namespace WebApi.BL.Implementations
{
	public class ReposBL : IReposBL
	{
		private IReposRepo _reposRepo;

		public ReposBL(IReposRepo reposRepo)
		{
			_reposRepo = reposRepo;
		}

		public IEnumerable<Repo> GetReposByKeyword(string keyword)
		{
			return _reposRepo.GetReposByKeyword(keyword)
				.ToList(); // TODO: explain tolisting here
		}
	}
}
