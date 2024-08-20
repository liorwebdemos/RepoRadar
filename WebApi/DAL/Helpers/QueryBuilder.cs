using System.Text;

namespace WebApi.DAL.Helpers
{
	public static class QueryBuilder
	{
		public static object BuildGraphQlQueryByFullNames(List<string> fullNames)
		{
			// dynamically build a GraphQl query
			StringBuilder stringBuilder = new();
			stringBuilder.Append("query {");
			int index = 1;
			foreach (string fullName in fullNames)
			{
				string[] parts = fullName.Split("/");
				if (parts.Length != 2)
				{
					throw new ArgumentException($"Invalid fullName format: {fullName}");
				}

				string owner = parts[0];
				string name = parts[1];

				stringBuilder.AppendLine($@"
                repo{index}: repository(owner: ""{owner}"", name: ""{name}"") {{
                    id
                    name
                    owner {{
                        login
                    }}
                    url
                    description
					createdAt
                }}");

				index++;
			}
			stringBuilder.Append("}");

			return new { query = stringBuilder.ToString() };
		}
	}
}
