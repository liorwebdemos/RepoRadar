using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BL.Contracts;
using WebApi.Models.Entities;
using WebApi.Models.Models;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthBL _authBL;

		public AuthController(IAuthBL authBL)
		{
			_authBL = authBL;
		}

		/// <summary>get current user's authentication (i.e. to find out if he's authenticated or not)<para />
		/// note: if not unauthenticated, instead of 401, we'd get a UserAuthentication (with isAuthenticated set to false)</summary>
		/// <returns>user authentication</returns>
		[AllowAnonymous]
		[HttpGet]
		public UserAuthentication GetUserAuthentication()
		{
			return _authBL.GetUserAuthentication();
		}

		/// <summary>login</summary>
		/// <param name="login"></param>
		/// <returns>user authentication</returns>
		[AllowAnonymous]
		[HttpPost]
		public Task<UserAuthentication> Authenticate(Login login)
		{
			return _authBL.Authenticate(login);
		}

		/// <summary>logout. if already logged out, returns an 401.</summary>
		/// <returns>user authentication (with isAuthenticated set to false)</returns>
		[Authorize] // we won't allow logout if already logged out (though, could have also allowed it)
		[HttpDelete]
		public Task<UserAuthentication> ResetUserAuthentication()
		{
			return _authBL.ResetUserAuthentication();
		}
	}
}
