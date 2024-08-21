import { UserAuthentication } from "./../models/user-authentication.model";
import { Component, OnInit } from "@angular/core";
import { catchError, Subscription, throwError } from "rxjs";
import { Router } from "@angular/router";
import { Login } from "../models/login.model";
import { AuthService } from "../auth.service";
import { CommonModule, DatePipe } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@Component({
	selector: "app-login",
	standalone: true,
	templateUrl: "./login.component.html",
	imports: [CommonModule, FormsModule, DatePipe],
	host: {
		class: "flex-grow flex flex-col",
	} /* https://stackoverflow.com/a/41528586 */,
})
export class LoginComponent implements OnInit {
	public loginDetails: Login = new Login();
	public lastLoginErrorDate: Date | null = null;

	constructor(
		private authService: AuthService,
		private router: Router,
	) {}

	ngOnInit(): void {
		// if already authenticated, redirect to home
		this.authService.getUserAuthentication().subscribe({
			next: (_userAuthentication: UserAuthentication | null) => {
				if (_userAuthentication?.isLoggedIn) {
					this.redirectToHome();
				}
			},
		});
	}

	public login(): Subscription {
		// no need to unsubscribe (https://stackoverflow.com/a/35043309, https://stackoverflow.com/a/56773683)
		return this.authService
			.login(this.loginDetails)
			.pipe(
				catchError((error) => {
					this.lastLoginErrorDate = new Date(); // shouldn't rly say "wrong username and/or password" in this scenario, but this is a dummy app
					console.error("Login failed:", error);
					return throwError(() => error); // re-throw
				}),
			)
			.subscribe({
				next: (_userAuthentication: UserAuthentication | null) =>
					_userAuthentication?.isLoggedIn
						? this.redirectToHome()
						: (this.lastLoginErrorDate = new Date()),
			});
	}

	private redirectToHome(): Promise<boolean> {
		return this.router.navigate(["./home"]); // https://stackoverflow.com/a/37622179
	}
}
