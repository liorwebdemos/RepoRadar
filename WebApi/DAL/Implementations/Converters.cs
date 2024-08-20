using WebApi.Models.Models;

namespace WebApi.DAL.Implementations
{
	//TODO: should just configure AutoMapper
	public static class Converters
	{
		public static Repo ConvertRepoDtoToRepo(this RepoRestDto restResponse)
		{
			if (restResponse == null) throw new ArgumentNullException();

			return new Repo()
			{
				Name = restResponse.Name,
				FullName = restResponse.FullName,
				Url = restResponse.HtmlUrl,
				Description = restResponse.Description,
				CreatedAt = restResponse.CreatedAt
			};
		}

		public static Repo ConvertRepoDtoToRepo(this RepoGraphQlDto graphQlResponse)
		{
			if (graphQlResponse == null) throw new ArgumentNullException();

			return new Repo()
			{
				Name = graphQlResponse.Name,
				FullName = $"{graphQlResponse.Owner}/{graphQlResponse.Name}",
				Url = graphQlResponse.Url,
				Description = graphQlResponse.Description,
				CreatedAt = graphQlResponse.CreatedAt
			};
		}
	}
}
