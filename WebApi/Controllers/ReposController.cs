using Microsoft.AspNetCore.Mvc;
using WebApi.BL.Contracts;
using WebApi.Models;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReposController : ControllerBase
	{
		private IReposBL _reposBL;

		public ReposController(IReposBL reposBL)
		{
			_reposBL = reposBL;
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
