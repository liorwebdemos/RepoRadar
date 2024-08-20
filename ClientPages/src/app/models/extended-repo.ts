import { Repo } from "./repo.model";

export class ExtendedRepo {
	constructor(repo: Repo, isFavorite: boolean) {
		if (!repo) {
			throw new Error();
		}
		this.repo = repo;
		this.isFavorite = !!isFavorite;
	}

	/** unique identifier (owner/name, i.e. facebook/react) */
	public get fullName(): string {
		return this.repo.fullName;
	}

	/** repo */
	public repo: Repo = new Repo();

	/** is favorite (for the current user)? */
	public isFavorite: boolean = false;
}
