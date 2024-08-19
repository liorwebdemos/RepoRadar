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

		[HttpGet]
		public IEnumerable<Repo> GetAllRepos(string keyword)
		{
			return _reposBL.GetReposByKeyword(keyword);
		}
	}
}
