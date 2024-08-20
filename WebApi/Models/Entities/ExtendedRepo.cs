namespace WebApi.Models.Models
{
	/// <summary>wrapper over repo, with app settings like favorability.<para />
	/// note: composition over inheritance</summary>
	public class ExtendedRepo
	{
		/// <summary>repo</summary>
		public Repo Repo { get; set; } = new Repo();

		/// <summary>is favorite (for the current user)?</summary>
		public bool IsFavorite { get; set; } = false;
	}
}
