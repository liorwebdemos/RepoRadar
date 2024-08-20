/** GitHub repository
 *
 * note: only part of the props of the full entity are provided in this model */
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
