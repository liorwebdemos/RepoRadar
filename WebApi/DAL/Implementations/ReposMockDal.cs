using WebApi.DAL.Contracts;
using WebApi.Models.Models;

namespace WebApi.DAL.Implementations
{
	public class ReposMockDal : IReposDal
	{
		// note: could've also placed the mock data in a json file or in the configuration
		private List<Repo> _repos = new()
		{
			new Repo { Name = "authentik", FullName = "goauthentik/authentik", CreatedAt = DateTimeOffset.Now, Description = "This is an extremely long description. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt." },
			new Repo { Name = "mpv", FullName = "mpv-player/mpv", CreatedAt = DateTimeOffset.Now, Description = "This is a short description." }
		};

		public Repo? GetRepoByFullName(string fullName)
		{
			return _repos
				.Where(r => r.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase))
				.SingleOrDefault();
		}

		public IEnumerable<Repo> GetReposByFullNames(List<string> fullNames)
		{
			return _repos
				.Where(r => fullNames.Contains(r.FullName, StringComparer.OrdinalIgnoreCase))
				.ToList();
		}

		public IEnumerable<Repo> GetReposByKeyword(string keyword)
		{
			return _repos
				.Where(r => r.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
				.ToList();
		}
	}
}
