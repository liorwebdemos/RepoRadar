using System.Text;
using System.Text.RegularExpressions;

namespace WebApi.DAL.Helpers
{
	/// <summary>note: in a real life app, would've used some 3rd party library to create this query<para />
	/// both for better security (even though we do escaping here) and to avoid this type of low level code</summary>
	public static class GraphQlQueryBuilder
	{
		private static readonly Regex ValidNameRegex = new("^[a-zA-Z0-9_-]+$", RegexOptions.Compiled);

		public static object BuildQueryByFullNames(List<string> fullNames)
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

				// Validate the owner and name using a strict regex
				if (!IsValidName(owner) || !IsValidName(name))
				{
					throw new ArgumentException($"Invalid characters in fullName: {fullName}");
				}

				stringBuilder.AppendLine($@"
                repo{index}: repository(owner: ""{EscapeString(owner)}"", name: ""{EscapeString(name)}"") {{
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

		private static bool IsValidName(string name)
		{
			// Ensure the name only contains valid characters
			return ValidNameRegex.IsMatch(name);
		}

		private static string EscapeString(string input)
		{
			// Escape any potentially harmful characters
			return input.Replace("\"", "\\\"").Replace("\\", "\\\\");
		}
	}
}
