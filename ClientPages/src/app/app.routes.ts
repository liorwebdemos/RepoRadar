import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { PageNotFoundComponent } from "./page-not-found/page-not-found.component";
import { authGuard } from "./auth.guard";
import { FavoritesComponent } from "./favorites/favorites.component";

export const routes: Routes = [
	{ path: "", component: LoginComponent },
	{ path: "home", component: HomeComponent, canActivate: [authGuard] },
	{
		path: "favorites",
		component: FavoritesComponent,
		canActivate: [authGuard],
	},
	{ path: "**", component: PageNotFoundComponent }, // wildcard route for a 404 page
];
