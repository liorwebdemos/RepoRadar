import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { UserAuthentication } from "./models/user-authentication.model";
import { Observable, switchMap, tap } from "rxjs";
import { API_BASE_URL } from "./app.config";
import { Login } from "./models/login.model";

@Injectable({
	providedIn: "root",
})
export class AuthService {
	constructor(
		private httpClient: HttpClient,
		@Inject(API_BASE_URL) private baseUrl: string,
	) {}

	public getUserAuthentication(): Observable<UserAuthentication> {
		return this.httpClient.get<UserAuthentication>(`${this.baseUrl}auth`, {
			withCredentials: true,
		});
	}

	public login(login: Login): Observable<UserAuthentication> {
		return this.httpClient.post<UserAuthentication>(
			`${this.baseUrl}auth`,
			login || {},
		);
	}

	public logout(): Observable<UserAuthentication> {
		return this.httpClient.delete<UserAuthentication>(`${this.baseUrl}auth`);
	}
}
