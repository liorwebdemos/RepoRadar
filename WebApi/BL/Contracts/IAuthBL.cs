using WebApi.Models.Entities;
using WebApi.Models.Models;

namespace WebApi.BL.Contracts
{
	public interface IAuthBL
	{
		UserAuthentication GetUserAuthentication();

		Task<UserAuthentication> Authenticate(Login login);

		Task<UserAuthentication> ResetUserAuthentication();
	}
}
