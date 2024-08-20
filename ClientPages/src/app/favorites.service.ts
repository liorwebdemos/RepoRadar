import { Inject, Injectable } from "@angular/core";
import { API_BASE_URL } from "./app.config";
import { HttpClient } from "@angular/common/http";

const LOCAL_STORAGE_KEY = "_RepoRadarFavoritesFullNames";

@Injectable({
	providedIn: "root",
})
export class FavoritesService {
	public getFavoritesFullNames(): string[] {
		const favoritesString = localStorage.getItem(LOCAL_STORAGE_KEY);
		if (!favoritesString) {
			return [];
		}
		return JSON.parse(favoritesString);
	}

	public addFavoriteByFullName(newFavoriteName: string): void {
		if (!newFavoriteName) {
			return;
		}
		const oldFavorites: string[] = this.getFavoritesFullNames();
		if (
			oldFavorites
				.map((of) => of.toLowerCase())
				.includes(newFavoriteName.toLowerCase())
		) {
			return;
		}
		localStorage.setItem(
			LOCAL_STORAGE_KEY,
			JSON.stringify(oldFavorites.concat(newFavoriteName)),
		);
	}

	public removeFavoriteByFullName(oldFavoriteName: string): void {
		if (!oldFavoriteName) {
			return;
		}
		const oldFavorites: string[] = this.getFavoritesFullNames();
		if (!oldFavorites.includes(oldFavoriteName)) {
			return;
		}
		localStorage.setItem(
			LOCAL_STORAGE_KEY,
			JSON.stringify(
				oldFavorites.filter(
					(of) => of.toLowerCase() !== oldFavoriteName.toLowerCase(),
				),
			),
		);
	}

	public get hasFavorites(): boolean {
		return !!this.getFavoritesFullNames()?.length;
	}
}
