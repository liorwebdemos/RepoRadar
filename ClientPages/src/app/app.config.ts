import {
	ApplicationConfig,
	InjectionToken,
	provideZoneChangeDetection,
} from "@angular/core";
import { provideRouter } from "@angular/router";
import { routes } from "./app.routes";
import { provideAnimationsAsync } from "@angular/platform-browser/animations/async";
import { provideHttpClient } from "@angular/common/http";

export const API_BASE_URL = new InjectionToken<string>("API_BASE_URL");

export const appConfig: ApplicationConfig = {
	providers: [
		provideZoneChangeDetection({ eventCoalescing: true }),
		provideRouter(routes),
		provideAnimationsAsync(),
		provideHttpClient(), // withInterceptors([userAuthenticationInterceptor])
		{ provide: API_BASE_URL, useValue: "https://localhost:7076/api/" }, // should put value(s) in environment configuration files
	],
};
