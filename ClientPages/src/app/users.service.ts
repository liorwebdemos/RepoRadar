import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { UserAuthentication } from "./models/user-authentication.model";
import { Observable, switchMap } from "rxjs";
import { API_BASE_URL } from "./app.config";
import { Login } from "./models/login.model";

@Injectable({
	providedIn: "root",
})
export class UsersService {
	constructor(
		private httpClient: HttpClient,
		private router: Router,
		@Inject(API_BASE_URL) private baseUrl: string,
	) {}

	public authenticate(login: Login): Observable<UserAuthentication> {
		return this.httpClient.post<UserAuthentication>(
			`${this.baseUrl}users/authentication`,
			login,
		);
	}

	private redirectToHome(): Promise<boolean> {
		return this.router.navigate(["./home"]); // https://stackoverflow.com/a/37622179
	}
}
