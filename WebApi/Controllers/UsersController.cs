using Microsoft.AspNetCore.Mvc;
using WebApi.BL.Contracts;
using WebApi.Models.Entities;
using WebApi.Models.Models;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/users")]
	public class UsersController : ControllerBase
	{
		private IUsersBL _usersBL;

		public UsersController(IUsersBL usersBL)
		{
			_usersBL = usersBL;
		}

		/// <summary></summary>
		/// <param name="login"></param>
		/// <returns></returns>
		[HttpPost("authentication")]
		public UserAuthentication Authenticate(Login login)
		{
			return _usersBL.Authenticate(login);
		}
	}
}
