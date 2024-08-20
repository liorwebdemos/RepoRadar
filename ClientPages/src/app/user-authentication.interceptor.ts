// import { inject } from "@angular/core";
// import {
// 	HttpInterceptorFn,
// 	HttpRequest,
// 	HttpHandlerFn,
// } from "@angular/common/http";
// import { UsersService } from "./users.service";

// // Convert the class-based interceptor to a functional interceptor
// export const userAuthenticationInterceptor: HttpInterceptorFn = (
// 	req: HttpRequest<unknown>,
// 	next: HttpHandlerFn,
// ) => {
// 	const usersService = inject(UsersService);
// 	const token = usersService.token;
// 	const authReq = token
// 		? req.clone({
// 				setHeaders: {
// 					Authorization: `Bearer ${token}`,
// 				},
// 			})
// 		: req;

// 	return next(authReq);
// };

// import { Injectable } from "@angular/core";
// import {
// 	HttpRequest,
// 	HttpHandler,
// 	HttpEvent,
// 	HttpInterceptor,
// } from "@angular/common/http";
// import { Observable } from "rxjs";
// import { UsersService } from "src/app/shared/services";

// /* https://dev-academy.com/how-to-use-angular-interceptors-to-manage-http-requests/ */
// @Injectable()
// export class UserAuthenticationInterceptor implements HttpInterceptor {
// 	constructor(private usersService: UsersService) {}

// 	public intercept(
// 		request: HttpRequest<unknown>,
// 		next: HttpHandler,
// 	): Observable<HttpEvent<unknown>> {
// 		return next.handle(this.enrichRequestWithAuthenticationToken(request));
// 	}

// 	private enrichRequestWithAuthenticationToken(
// 		request: HttpRequest<unknown>,
// 	): HttpRequest<unknown> {
// 		const token = this.usersService.token;
// 		if (!token) {
// 			return request;
// 		}
// 		return request.clone({
// 			setHeaders: {
// 				Authorization: `Bearer ${token}`,
// 			},
// 		});
// 	}
// }
