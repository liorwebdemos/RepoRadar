using WebApi.DAL.Contracts;
using WebApi.Models;

namespace WebApi.DAL.Implementations
{
	public class ReposMockRepo : IReposRepo
	{
		// TODO: move to json file? configuration?
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
