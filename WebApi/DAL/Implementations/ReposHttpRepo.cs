using WebApi.DAL.Contracts;
using WebApi.Models;

namespace WebApi.DAL.Implementations
{
	public class ReposHttpRepo : IReposRepo
	{
		public IEnumerable<Repo> GetReposByKeyword(string keyword)
		{
			throw new NotImplementedException();
		}
	}
}
