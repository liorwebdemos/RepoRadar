using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using WebApi.DAL.Contracts;
using WebApi.Models;

namespace WebApi.DAL.Implementations
{
	/// <summary>
	/// note high/low level code separation
	/// </summary>
	public class ReposHttpRepo : IReposRepo
	{
		private string BaseUrl { get; }
		private string AppName { get; }
		private HttpClient Client { get; } // abstraction. we don't allow direct access from outside.

		public ReposHttpRepo(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			BaseUrl = configuration.GetValue<string>("PublicAPIs:GitHub:BaseUrl")!; //TODO: create a type-safe AppSettings class?
			AppName = webHostEnvironment.ApplicationName;

			HttpClientHandler handler = new();
			Client = new HttpClient(handler);
			Client.BaseAddress = new Uri(BaseUrl);
		}

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
			request.Headers.Add("Accept", "application/json");
			request.Headers.Add("User-Agent", AppName); // reasoning: https://docs.github.com/en/rest/using-the-rest-api/getting-started-with-the-rest-api?apiVersion=2022-11-28#user-agent

			if (requestBody != null)
			{
				string requestBodyAsJson = JsonConvert.SerializeObject(requestBody);
				request.Content = new StringContent(requestBodyAsJson, Encoding.UTF8, "application/json");
			}

			HttpResponseMessage response = Client.Send(request);
			if (!response.IsSuccessStatusCode)
			{
				throw new InvalidOperationException("http client: response came back with non-successful status code"); // in a real project, we'd probably catch and re-throw via some global, project-wide, logging middleware...
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

		public IEnumerable<Repo> GetReposByKeyword(string keyword)
		{
			GitHubReposResponse? response = SendSimpleHttpRequest<GitHubReposResponse?>($"search/repositories?q={keyword}", HttpMethod.Get); // could have also used the UriBuilder to avoid concatenating the query string param like so...
			if (response == null || response.Items == null)
			{
				return Enumerable.Empty<Repo>();
			}
			return response.Items;
		}
	}
}
