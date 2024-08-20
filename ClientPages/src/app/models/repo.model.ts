/** GitHub repository - server model not to be used by the client's public APIs - since this model is missing the isFavorite property */
export class Repo {
	/**  */
	public id: number = 0;

	/**  */
	public name: string = "";

	/**  */
	public fullName: string = "";

	/**  */
	public htmlUrl: string = "";

	/**  */
	public description: string = "";

	/**  */
	public createdAt?: Date;

	/**  */
	public language: string = "";
}
