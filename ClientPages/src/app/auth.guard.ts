import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "./auth.service";
import { UserAuthentication } from "./models/user-authentication.model";
import { catchError, map, of } from "rxjs";

/** check user's authentication before fully activating the route
 *
 * note: in a real app we probably would't add this ASYNC guard to each and every page...
 *
 * we'd add a SYNC guard that doesn't require a roundtrip to the server, relying on locally saved data */
export const authGuard: CanActivateFn = (route, state) => {
	const authService = inject(AuthService);
	const router = inject(Router);

	return authService.getUserAuthentication().pipe(
		map((_userAuthentication: UserAuthentication) => {
			if (_userAuthentication?.isLoggedIn) {
				return true;
			}
			router.navigate(["/"]);
			return false;
		}),
		catchError(() => {
			router.navigate(["/"]);
			return of(false);
		}),
	);
};
