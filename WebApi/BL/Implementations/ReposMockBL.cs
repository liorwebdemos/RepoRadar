using WebApi.BL.Contracts;
using WebApi.Models;

namespace WebApi.BL.Implementations
{
	public class ReposMockBL : IReposBL
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
				.ToList(); // ToListing here to exemplify future performance tracking - BL should complete its job
		}
	}
}
