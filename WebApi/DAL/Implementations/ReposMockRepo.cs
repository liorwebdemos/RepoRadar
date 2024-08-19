using WebApi.DAL.Contracts;
using WebApi.Models;

namespace WebApi.DAL.Implementations
{
	public class ReposMockRepo : IReposRepo
	{
		// note: can have also placed the mock data in a json file or in the configuration
		private List<Repo> _repos = new()
		{
			new Repo { Name = "hello" },
			new Repo { Name = "world" }
		};

		public IEnumerable<Repo> GetReposByKeyword(string keyword)
		{
			return _repos
				.Where(r => r.Name.Contains(keyword))
				.ToList();
		}
	}
}
