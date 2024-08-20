import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { UserAuthentication } from "./models/user-authentication.model";
import { Observable, switchMap } from "rxjs";
import { API_BASE_URL } from "./app.config";

@Injectable({
	providedIn: "root",
})
export class SessionService {
	constructor(
		private httpClient: HttpClient,
		private router: Router,
		@Inject(API_BASE_URL) private baseUrl: string,
	) {}

	/** "anonymous authentication" */
	public createSession(): Observable<UserAuthentication> {
		return this.httpClient.post<UserAuthentication>(
			`${this.baseUrl}session`,
			{},
		); // POST seems more appropriate even though we send an empty body
	}

	/** throw away any existing authentication, and re-authenticate */
	public resetSession(): Observable<boolean> {
		return this.httpClient
			.delete<UserAuthentication>(`${this.baseUrl}session`)
			.pipe(switchMap(() => this.router.navigate([""])));
	}
}

// const LOCAL_STORAGE_TOKEN_KEY = "_RepoRadarToken";

// public get token(): string | null {
// 	// for now we'll keep just the token in the LS // TODO: use an expirable local storage (i.e. https://github.com/grevory/angular-local-storage)
// 	return localStorage.getItem(LOCAL_STORAGE_TOKEN_KEY);
// }
// public set token(value: string | null) {
// 	if (value) {
// 		localStorage.setItem(LOCAL_STORAGE_TOKEN_KEY, value);
// 	} else {
// 		localStorage.removeItem(LOCAL_STORAGE_TOKEN_KEY);
// 	}
// }
// public get isLoggedIn(): boolean {
// 	return !!this.token;
// }

// /** get logged-in user's personal details */
// public getPersonalDetails(): Observable<PersonalDetailsModel> {
// 	// short on time, I won't create this as a real separate function
// 	if (this.personalDetails) {
// 		return observableOf(this.personalDetails);
// 	}
// 	return this.httpClient
// 		.get<PersonalDetailsModel>(
// 			"https://localhost:44300/api/users/personal-details",
// 		)
// 		.pipe(
// 			map(
// 				(response) =>
// 					(this.personalDetails = plainToInstance(
// 						PersonalDetailsModel,
// 						response,
// 					)),
// 			),
// 		);
// }
