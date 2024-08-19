import { Routes } from "@angular/router";
import { PageNotFoundComponent } from "./page-not-found/page-not-found.component";
import { HomeComponent } from "./home/home.component";

export const routes: Routes = [
	{ path: "", component: HomeComponent },
	// { path: "about", component: AboutComponent },
	{ path: "**", component: PageNotFoundComponent }, // Wildcard route for a 404 page
];
