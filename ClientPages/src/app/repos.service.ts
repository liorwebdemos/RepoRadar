import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import {
	BehaviorSubject,
	map,
	Observable,
	ObservableInput,
	switchMap,
	tap,
} from "rxjs";
import { ExtendedRepo } from "./models/extended-repo";
import { API_BASE_URL } from "./app.config";
import { plainToInstance } from "class-transformer";

@Injectable({
	providedIn: "root",
})
export class ReposService {
	private filteredExtendedReposSubject$ = new BehaviorSubject<ExtendedRepo[]>(
		[],
	);
	public filteredExtendedRepos$ =
		this.filteredExtendedReposSubject$.asObservable();

	private favoriteExtendedReposSubject$ = new BehaviorSubject<ExtendedRepo[]>(
		[],
	);
	public favoriteExtendedRepos$ =
		this.favoriteExtendedReposSubject$.asObservable();

	constructor(
		private httpClient: HttpClient,
		@Inject(API_BASE_URL) private baseUrl: string,
	) {}

	/** note: not used at the moment, but part of our public API */
	private getExtendedReposByFullNames(
		fullNames: string[],
	): Observable<ExtendedRepo[]> {
		return this.httpClient
			.post<ExtendedRepo[]>(`${this.baseUrl}repos`, fullNames)
			.pipe(map((repo) => plainToInstance(ExtendedRepo, repo)));
	}

	public getExtendedReposByKeyword(
		keyword: string,
	): Observable<ExtendedRepo[]> {
		const params = new HttpParams().set("keyword", keyword);
		return this.httpClient
			.get<ExtendedRepo[]>(`${this.baseUrl}repos`, { params })
			.pipe(
				map((repos) => repos.map((r) => plainToInstance(ExtendedRepo, r))),
				tap((repos) => this.filteredExtendedReposSubject$.next(repos)),
			);
	}

	public getFavoriteExtendedRepos(): Observable<ExtendedRepo[]> {
		return this.httpClient
			.get<ExtendedRepo[]>(`${this.baseUrl}repos/favorites`)
			.pipe(
				map((repos) => repos.map((r) => plainToInstance(ExtendedRepo, r))),
				tap((repos) => this.favoriteExtendedReposSubject$.next(repos)),
			);
	}

	public setFavoriteByFullName(fullName: string): Observable<ExtendedRepo> {
		if (!fullName) {
			throw new Error("Repo full name (required parameter) is empty");
		}

		const itemThatAlreadyIsInsideFavorites =
			this.favoriteExtendedReposSubject$.value.find(
				(r) => r.fullName.toLowerCase() === fullName.toLowerCase(),
			);
		if (itemThatAlreadyIsInsideFavorites) {
			throw new Error("Repo is already inside of favorites");
		}

		return this.httpClient
			.post<ExtendedRepo>(
				`${this.baseUrl}repos/favorites?fullName=${fullName}`, //can't use HttpParams for a post request here
				{},
			)
			.pipe(
				map((repo) => plainToInstance(ExtendedRepo, repo)),
				tap((repo) =>
					this.favoriteExtendedReposSubject$.next(
						this.favoriteExtendedReposSubject$.value.concat(repo),
					),
				),
			);
	}

	public setUnfavoriteByFullName(fullName: string): Observable<ExtendedRepo> {
		if (!fullName) {
			throw new Error("Repo full name (required parameter) is empty");
		}

		const itemThatAlreadyIsOutsideFavorites =
			this.favoriteExtendedReposSubject$.value.find(
				(r) => r.fullName === fullName,
			);
		if (itemThatAlreadyIsOutsideFavorites) {
			throw new Error("Repo is already outside of favorites");
		}

		const params = new HttpParams().set("fullName", fullName);
		return this.httpClient
			.delete<ExtendedRepo>(`${this.baseUrl}repos/favorites`, {
				params,
			})
			.pipe(
				map((response) => plainToInstance(ExtendedRepo, response)),
				tap((repo) =>
					this.filteredExtendedReposSubject$.next(
						this.filteredExtendedReposSubject$.value.filter(
							(r) =>
								r.fullName.toLowerCase() !==
								repo.fullName.toLowerCase(),
						),
					),
				),
			);
	}
}
