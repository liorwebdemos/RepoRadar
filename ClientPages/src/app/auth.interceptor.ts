import { HttpInterceptorFn } from "@angular/common/http";

/** this is the "with credentials interceptor", to make sure we send credentials in CORS requests */
export const authInterceptor: HttpInterceptorFn = (req, next) => {
	const clonedRequest = req.clone({ withCredentials: true });
	return next(clonedRequest);
};
