using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.BL.Contracts;
using WebApi.Models.Entities;
using WebApi.Models.Models;

namespace WebApi.BL.Implementations
{
	public class AuthBL : IAuthBL
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserAuthentication _unauthenticated = new()
		{
			IsLoggedIn = false // false is already the default value so no real need to set it; just being more explicit
		};

		public AuthBL(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<UserAuthentication> Authenticate(Login login)
		{
			// if already logged in, logout first
			if (GetUserAuthentication().IsLoggedIn)
			{
				await _httpContextAccessor.HttpContext!.SignOutAsync();
			}

			if (login == null || string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
			{
				return _unauthenticated; // another approach would've been to return null
			}

			string validUsername = _configuration["Credentials:Username"]!;
			string validPassword = _configuration["Credentials:Password"]!;
			// in a real app this string comparison wouldn't have happened. we'd maybe use .net core identity and sign in via the SignInManager
			if (login.Username != validUsername || login.Password != validPassword)
			{
				return _unauthenticated; // another approach would've been to return null
			}

			await _httpContextAccessor.HttpContext!.SignInAsync(new ClaimsPrincipal(CreateClaimsIdentity(login.Username)));

			return new UserAuthentication()
			{
				IsLoggedIn = true,
				Username = login.Username,
			};
		}

		// note: under a different approach, if the user is not authenticated,
		// we could've just let the .net web api pipeline to return an 401 by adding the [Authorize] attribute to the endpoint
		// but since we chose to AllowAnonymous and return UserAuthentication anyway (even if not authenticated),
		// we do actually need to check for HttpContext.User.Identity.IsAuthenticated
		public UserAuthentication GetUserAuthentication()
		{
			if (_httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true)
			{
				return new UserAuthentication()
				{
					IsLoggedIn = true,
					Username = _httpContextAccessor.HttpContext!.User.Identity!.Name,
				};
			}
			return _unauthenticated;
		}

		public async Task<UserAuthentication> ResetUserAuthentication()
		{
			await _httpContextAccessor.HttpContext!.SignOutAsync();
			return _unauthenticated;
		}

		// for JWT creation on login (= transmit the JWT to the client in the response, in whatever strategies that were defined in Program.cs - in this case cookie, not bearer)
		private static ClaimsIdentity CreateClaimsIdentity(string username)
		{
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, username),
				new Claim(JwtRegisteredClaimNames.Sub, username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};
			return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
