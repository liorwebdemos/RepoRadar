using Microsoft.AspNetCore.Mvc;
using WebApi.BL.Contracts;
using WebApi.Models.Models;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/repos")]
	public class ReposController : ControllerBase
	{
		private IReposBL _reposBL;

		public ReposController(IReposBL reposBL)
		{
			_reposBL = reposBL;
		}

		/// <summary>get extended repos by keyword</summary>
		/// <param name="keyword">search query</param>
		/// <returns>list of extended repos</returns>
		[HttpGet]
		public IEnumerable<ExtendedRepo> GetExtendedReposByKeyword(string? keyword)
		{
			return _reposBL.GetExtendedReposByKeyword(keyword);
		}

		[HttpPost]
		public IEnumerable<ExtendedRepo> GetExtendedReposByFullNames([FromBody] List<string> fullNames)
		{
			return _reposBL.GetExtendedReposByFullNames(fullNames);
		}

		/// <summary>get favorite extended repos (for the current user)</summary>
		[HttpGet("favorites")]
		public IEnumerable<ExtendedRepo> GetFavoriteExtendedRepos()
		{
			return _reposBL.GetFavoriteExtendedRepos();
		}

		/// <summary>add to favorites (if already there, keeps it that way)</summary>
		/// <param name="fullName">repo full name (owner/name), i.e. facebook/react</param>
		/// <returns>final extended repo</returns>
		[HttpPost("favorites")]
		public ExtendedRepo SetFavoriteById([FromQuery] string fullName) // TODO: change to repo full name
		{
			return _reposBL.SetFavoriteByFullName(fullName);
		}

		/// <summary>remove from favorites (if not there, keeps it that way)</summary>
		/// <param name="fullName">repo full name (owner/name), i.e. facebook/react</param>
		/// <returns>final extended repo</returns>
		[HttpDelete("favorites")]
		public ExtendedRepo SetUnfavoriteById([FromQuery] string fullName) // TODO: change to repo full name
		{
			return _reposBL.SetUnfavoriteByFullName(fullName);
		}
	}
}
