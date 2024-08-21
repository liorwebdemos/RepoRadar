import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { BehaviorSubject, filter, map, Observable, of, skip, tap } from "rxjs";
import { ExtendedRepo } from "./models/extended-repo";
import { API_BASE_URL } from "./app.config";
import { plainToInstance } from "class-transformer";
import { Repo } from "./models/repo.model";

const LOCAL_STORAGE_FAVORITES_KEY = "_RepoRadarFavoritesFullNames";

@Injectable({
	providedIn: "root",
})
export class ReposService {
	//#region props
	private filteredExtendedReposSubject$ = new BehaviorSubject<ExtendedRepo[]>(
		[],
	);
	private favoriteExtendedReposSubject$ = new BehaviorSubject<ExtendedRepo[]>(
		[],
	);

	public filteredExtendedRepos$ =
		this.filteredExtendedReposSubject$.asObservable();

	public favoriteExtendedRepos$ =
		this.favoriteExtendedReposSubject$.asObservable();
	//#endregion

	constructor(
		private httpClient: HttpClient,
		@Inject(API_BASE_URL) private baseUrl: string,
	) {}

	//#region public api - handles only with extended repo
	public updateExtendedReposByKeyword(
		keyword: string | null,
	): Observable<ExtendedRepo[]> {
		if (!keyword) {
			this.filteredExtendedReposSubject$.next([]);
			return of([]);
		}
		const params = new HttpParams().set("keyword", keyword);
		return this.httpClient
			.get<Repo[]>(`${this.baseUrl}repos`, { params })
			.pipe(
				map(
					// map a repo -> to an extended repo
					(repos) =>
						repos.map(
							(r) =>
								new ExtendedRepo(
									plainToInstance(Repo, r),
									this.isSavedInFavoriteByFullName(r.fullName),
								),
						),
				),
				tap((repos) => this.filteredExtendedReposSubject$.next(repos)),
			);
	}

	/** should be called once, at the start */
	public initializeFavoriteExtendedRepos(): Observable<ExtendedRepo[]> {
		return this.getReposByFullNames(this.getSavedFavoritesFullNames()).pipe(
			map(
				// map a repo -> to an extended repo
				(repos) => repos.map((r) => new ExtendedRepo(r, true)),
			),
			tap((repos) => this.favoriteExtendedReposSubject$.next(repos)),
		);
	}

	public setFavorite(extendedRepo: ExtendedRepo): void {
		if (!extendedRepo?.fullName) {
			throw new Error("Repo full name (required parameter) is empty");
		}
		if (this.isSavedInFavoriteByFullName(extendedRepo.fullName)) {
			throw new Error("Repo is already inside of favorites");
		}

		// 1. save in LS
		this.saveFavoriteByFullName(extendedRepo.fullName);
		// 2. add to favorites array
		this.favoriteExtendedReposSubject$.next([
			// add to the beginning of the favorites array
			new ExtendedRepo(extendedRepo.repo, true),
			...this.favoriteExtendedReposSubject$.value,
		]);
		// 3. change isFavorite in filtered array
		this.filteredExtendedReposSubject$.next(
			this.filteredExtendedReposSubject$.value.map((er) => {
				if (
					er.fullName.toLowerCase() === extendedRepo.fullName.toLowerCase()
				) {
					return new ExtendedRepo(er.repo, true);
				}
				return er; // return the original object if the condition is not met
			}),
		);
	}

	public setUnfavorite(extendedRepo: ExtendedRepo): void {
		if (!extendedRepo?.fullName) {
			throw new Error("Repo full name (required parameter) is empty [1]");
		}
		if (!this.isSavedInFavoriteByFullName(extendedRepo.fullName)) {
			throw new Error("Repo is already outside of favorites");
		}

		// 1. unsave in LS
		this.unsaveFavoriteByFullName(extendedRepo.fullName);
		// 2. remove from favorites array
		this.favoriteExtendedReposSubject$.next(
			this.favoriteExtendedReposSubject$.value.filter(
				(r) =>
					r.fullName.toLowerCase() !== extendedRepo.fullName.toLowerCase(),
			),
		);
		// 3. change isFavorite in filtered array
		this.filteredExtendedReposSubject$.next(
			this.filteredExtendedReposSubject$.value.map((er) => {
				if (
					er.fullName.toLowerCase() === extendedRepo.fullName.toLowerCase()
				) {
					return new ExtendedRepo(er.repo, false);
				}
				return er; // return the original object if the condition is not met
			}),
		);
	}
	//#endregion

	/** access server-side public api, but this isn't a part of the client public api */
	private getReposByFullNames(fullNames: string[]): Observable<Repo[]> {
		return this.httpClient
			.post<Repo[]>(`${this.baseUrl}repos`, fullNames)
			.pipe(map((repos) => repos.map((r) => plainToInstance(Repo, r))));
	}

	//#region saved local storage data - favorite repos names
	private getSavedFavoritesFullNames(): string[] {
		const favoritesString = localStorage.getItem(LOCAL_STORAGE_FAVORITES_KEY);
		if (!favoritesString) {
			return [];
		}
		return JSON.parse(favoritesString);
	}

	private saveFavoriteByFullName(newFavoriteName: string): void {
		if (!newFavoriteName) {
			return;
		}
		const oldFavoritesNames: string[] = this.getSavedFavoritesFullNames();
		if (this.isSavedInFavoriteByFullName(newFavoriteName)) {
			return;
		}
		localStorage.setItem(
			LOCAL_STORAGE_FAVORITES_KEY,
			JSON.stringify(oldFavoritesNames.concat(newFavoriteName)),
		);
	}

	private unsaveFavoriteByFullName(oldFavoriteName: string): void {
		if (!oldFavoriteName) {
			return;
		}
		const oldFavoritesNames: string[] = this.getSavedFavoritesFullNames();
		if (!oldFavoritesNames.includes(oldFavoriteName)) {
			return;
		}
		localStorage.setItem(
			LOCAL_STORAGE_FAVORITES_KEY,
			JSON.stringify(
				oldFavoritesNames.filter(
					(of) => of.toLowerCase() !== oldFavoriteName.toLowerCase(),
				),
			),
		);
	}

	public get hasSavedFavorites(): boolean {
		return !!this.getSavedFavoritesFullNames()?.length;
	}

	private isSavedInFavoriteByFullName(fullName: string): boolean {
		if (!fullName) {
			throw new Error("Repo full name (required parameter) is empty [2]");
		}
		const favorites: string[] = this.getSavedFavoritesFullNames();
		if (
			favorites
				.map((of) => of.toLowerCase())
				.includes(fullName.toLowerCase())
		) {
			return true;
		}
		return false;
	}
	//#endregion
}
