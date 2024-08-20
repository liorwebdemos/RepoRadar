using WebApi.Models.Entities;
using WebApi.Models.Models;

namespace WebApi.BL.Contracts
{
	public interface IUsersBL
	{
		public UserAuthentication Authenticate(Login login)
		{
			if (123 == 456)
			{
				throw new UnauthorizedAccessException();
			}
			string token = "123";
			return new UserAuthentication()
			{
				Token = token
			};
		}
	}
}
