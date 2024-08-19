using WebApi.Models;

namespace WebApi.DAL.Contracts
{
	public interface IReposRepo
	{
		//// NOTE: we could've combined the two to one single method with optional parameter, but I've decided to split into two anyway.
		///// <summary>get repos</summary>
		//IEnumerable<Repo> GetRepos();

		/// <summary>get repos by keyword</summary>
		IEnumerable<Repo> GetReposByKeyword(string keyword);
	}
}
