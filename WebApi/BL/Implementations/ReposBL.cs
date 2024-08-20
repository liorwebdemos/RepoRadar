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

		public IEnumerable<ExtendedRepo> GetExtendedReposByKeyword(string? keyword)
		{
			if (string.IsNullOrWhiteSpace(keyword))
			{
				return Enumerable.Empty<ExtendedRepo>();
			}
			return ReposRepo.GetReposByKeyword(keyword)
				.OrderBy(r => r.Name)
				.Select(r => new ExtendedRepo // no need for AutoMapper just for this
				{
					Repo = r,
					IsFavorite = false //IsFavoriteByFullName(r.FullName)
				})
				.ToList(); // TODO: explain reasoning behind tolisting here
		}

		public IEnumerable<ExtendedRepo> GetExtendedReposByFullNames(List<string> fullNames)
		{
			if (fullNames.IsNullOrEmpty())
			{
				return Enumerable.Empty<ExtendedRepo>();
			}
			return ReposRepo.GetReposByFullNames(fullNames)
				.Select(r => new ExtendedRepo()
				{
					Repo = r,
					IsFavorite = true // TODO: check
				});
		}

		//public IEnumerable<ExtendedRepo> GetFavoriteExtendedRepos()
		//{
		//	// TODO: get favorites ids from claims
		//	List<string> favoritesFullNames = new(){
		//		"goauthentik/authentik",
		//		"mpv-player/mpv"
		//	};

		//	IEnumerable<Repo> favoriteRepos = ReposRepo.GetReposByFullNames(favoritesFullNames);

		//	return favoriteRepos.Select(fr => new ExtendedRepo()
		//	{
		//		Repo = fr,
		//		IsFavorite = true
		//	});
		//}

		//public ExtendedRepo SetFavoriteByFullName(string fullName)
		//{
		//	if (string.IsNullOrWhiteSpace(fullName))
		//	{
		//		throw new InvalidOperationException();
		//	}
		//	Repo? repo = ReposRepo.GetRepoByFullName(fullName);
		//	if (repo == null)
		//	{
		//		throw new ArgumentException();
		//	}
		//	//TODO: add to claims
		//	return new ExtendedRepo
		//	{
		//		Repo = repo,
		//		IsFavorite = true
		//	};
		//}

		//public ExtendedRepo SetUnfavoriteByFullName(string fullName)
		//{
		//	if (string.IsNullOrWhiteSpace(fullName))
		//	{
		//		throw new InvalidOperationException();
		//	}
		//	Repo? repo = ReposRepo.GetRepoByFullName(fullName);
		//	if (repo == null)
		//	{
		//		throw new ArgumentException();
		//	}
		//	//TODO: remove from claims
		//	return new ExtendedRepo
		//	{
		//		Repo = repo,
		//		IsFavorite = false
		//	};
		//}

		//private bool IsFavoriteByFullName(string fullName)
		//{
		//	return false; // TODO
		//}
	}
}
