/** GitHub repository - server model not to be used by the client's public APIs - since this model is missing the isFavorite property */
export class Repo {
	/**  */
	public name: string = "";

	/**  */
	public fullName: string = "";

	/**  */
	public url: string = "";

	/**  */
	public description: string = "";

	/**  */
	public createdAt?: Date;
}
