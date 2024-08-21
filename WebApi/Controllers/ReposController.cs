using Microsoft.AspNetCore.Mvc;
using WebApi.BL.Contracts;
using WebApi.Models.Models;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/repos")]
	public class ReposController : ControllerBase
	{
		private readonly IReposBL _reposBL;

		public ReposController(IReposBL reposBL)
		{
			_reposBL = reposBL;
		}

		/// <summary>get repos by full names</summary>
		/// <param name="fullNames"></param>
		/// <returns></returns>
		[HttpPost]
		public IEnumerable<Repo> GetReposByFullNames([FromBody] List<string> fullNames)
		{
			return _reposBL.GetReposByFullNames(fullNames);
		}

		/// <summary>get repos by keyword</summary>
		/// <param name="keyword">search query</param>
		/// <returns>list of repos</returns>
		[HttpGet]
		public IEnumerable<Repo> GetReposByKeyword(string? keyword)
		{
			return _reposBL.GetReposByKeyword(keyword);
		}
	}
}
