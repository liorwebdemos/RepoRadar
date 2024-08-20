using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;
using WebApi.DAL.Contracts;
using WebApi.DAL.Helpers;
using WebApi.Models.DTOs;
using WebApi.Models.Models;

namespace WebApi.DAL.Implementations
{
	/// <summary>note high/low level code separation</summary>
	public class ReposHttpDal : IReposDal
	{
		// abstraction. we don't allow direct access from outside.
		private string AppName { get; }
		private HttpClient Client { get; }
		private string BaseUrl { get; }
		private string AccessToken { get; }

		public ReposHttpDal(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			AppName = webHostEnvironment.ApplicationName;
			BaseUrl = configuration.GetValue<string>("GitHubApi:BaseUrl")!; //TODO: create a type-safe AppSettings class?
			AccessToken = configuration.GetValue<string>("GitHubApi:AccessToken")!; //TODO: create a type-safe AppSettings class?

			HttpClientHandler restHandler = new();
			Client = new HttpClient(restHandler);
			Client.BaseAddress = new Uri(BaseUrl);
		}

		#region Higher Level Logics
		public Repo? GetRepoByFullName(string fullName)
		{
			RepoRestDto? response = SendSimpleHttpRequest<RepoRestDto?>($"repos/{fullName}", HttpMethod.Get);
			if (response == null)
			{
				return default;
			}
			return response.ConvertRepoDtoToRepo();
		}

		public IEnumerable<Repo> GetReposByFullNames(List<string> fullNames)
		{
			object query = GraphQlQueryBuilder.BuildQueryByFullNames(fullNames);

			ReposGraphQlDto? response = SendSimpleHttpRequest<ReposGraphQlDto?>("graphql", HttpMethod.Post, query);
			if (response == null || response.Data.IsNullOrEmpty())
			{
				return Enumerable.Empty<Repo>();
			}

			return response.Data.Values
				.Select(r => r.ConvertRepoDtoToRepo())
				.ToList();
		}

		public IEnumerable<Repo> GetReposByKeyword(string keyword)
		{
			ReposRestDto? response = SendSimpleHttpRequest<ReposRestDto?>($"search/repositories?q={keyword}", HttpMethod.Get); // could have also used the UriBuilder to avoid concatenating the query string param like so...
			if (response == null || response.Items == null)
			{
				return Enumerable.Empty<Repo>();
			}
			return response.Items
				.Select(r => r.ConvertRepoDtoToRepo())
				.ToList();
		}
		#endregion Higher Level Logics

		#region Lower Level Logics - Could Also Move Elsewhere
		/// <summary>
		/// send a simple (w/o custom headers etc) http request
		/// note 1: this is low level code (serializations, casings manipulation, adding fixed headers, etc) and therefore we've put it in a private helper func (also, to avoid code repetition as more public methods are added).
		/// note 2: this does not include adding headers etc and much more. as the name suggests, currently for simple requests only.
		/// note 3: could also be created as an extension method in a static class... but decided not to go overboard.
		/// </summary>
		/// <typeparam name="T">the type of the object you are expecting to receive back</typeparam>
		/// <param name="apiEndpointUrlSuffix">the part after the base url</param>
		/// <param name="httpMethod">http method</param>
		/// <param name="requestBody">optional</param>
		/// <returns></returns>
		/// <exception cref="InvalidDataException"></exception>
		private T? SendSimpleHttpRequest<T>(string apiEndpointUrlSuffix, HttpMethod httpMethod, object? requestBody = null)
		{
			HttpRequestMessage request = new(httpMethod, apiEndpointUrlSuffix);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request.Headers.UserAgent.Add(new ProductInfoHeaderValue($"{AppName}HttpClient", "1.0")); // reasoning: https://docs.github.com/en/rest/using-the-rest-api/getting-started-with-the-rest-api?apiVersion=2022-11-28#user-agent
			if (!AccessToken.IsNullOrEmpty())
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
			}

			if (requestBody != null)
			{
				string requestBodyAsJson = JsonConvert.SerializeObject(requestBody);
				request.Content = new StringContent(requestBodyAsJson, Encoding.UTF8, "application/json");
			}

			HttpResponseMessage response = Client.Send(request);
			if (!response.IsSuccessStatusCode)
			{
				throw new InvalidDataException("http client: response came back with non-successful status code"); // in a real project, we'd probably catch and re-throw via some global, project-wide, logging middleware...
			}

			string responseBody = response.Content.ReadAsStringAsync().Result;
			JsonSerializerSettings settings = new()
			{
				// snake_case -> PascalCase
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new SnakeCaseNamingStrategy()
				}
			};
			T? responseBodyAsObject = JsonConvert.DeserializeObject<T>(responseBody, settings);

			return responseBodyAsObject;
		}
		#endregion Lower Level Logics - Could Also Move Elsewhere
	}
}
