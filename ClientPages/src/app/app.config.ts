import {
	ApplicationConfig,
	InjectionToken,
	provideZoneChangeDetection,
} from "@angular/core";
import { provideRouter } from "@angular/router";
import { routes } from "./app.routes";
import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { authInterceptor } from "./auth.interceptor";

export const API_BASE_URL = new InjectionToken<string>("API_BASE_URL");

export const appConfig: ApplicationConfig = {
	providers: [
		provideZoneChangeDetection({ eventCoalescing: true }),
		provideRouter(routes),
		provideHttpClient(withInterceptors([authInterceptor])),
		{ provide: API_BASE_URL, useValue: "https://localhost:7076/api/" }, // should put value(s) in environment configuration files
	],
};
