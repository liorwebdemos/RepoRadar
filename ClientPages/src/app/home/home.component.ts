import { ReposService } from "./../repos.service";
import { Component } from "@angular/core";
import { HeaderComponent } from "../header/header.component";
import { SearchBarComponent } from "../search-bar/search-bar.component";
import { CommonModule } from "@angular/common";
import {
	BehaviorSubject,
	debounceTime,
	filter,
	Subject,
	switchMap,
	takeUntil,
	tap,
} from "rxjs";
import { ExtendedRepo } from "../models/extended-repo";
import { RepoBoxComponent } from "../repo-box/repo-box.component";
import { MenuComponent } from "../menu/menu.component";
import { FavoritesComponent } from "../favorites/favorites.component";

@Component({
	selector: "app-home",
	standalone: true,
	imports: [
		HeaderComponent,
		SearchBarComponent,
		CommonModule,
		RepoBoxComponent,
		MenuComponent,
		FavoritesComponent,
	],
	templateUrl: "./home.component.html",
	host: { class: "flex-grow flex flex-col overflow-auto" },
})
export class HomeComponent {
	private destroySubject$ = new Subject<void>();
	private keywordSubject$ = new BehaviorSubject<string>(""); // gets updated in real time
	public keywordAfterResult: string | null = null; // note that we have results stream and keyword stream; we want a final stream where the keyword gets updated after the results have.
	public isAfterInitialLoad = false;

	constructor(public reposService: ReposService) {}

	ngOnInit(): void {
		// first, debounce the keyword input
		this.keywordSubject$
			.pipe(
				filter((keyword) => (this.isAfterInitialLoad ? true : !!keyword)),
				debounceTime(3000),
				// then, switchMap directly after the debounce time
				switchMap((keyword) =>
					this.reposService.updateExtendedReposByKeyword(keyword).pipe(
						tap((_repos: ExtendedRepo[]) => {
							this.keywordAfterResult = keyword;
						}),
					),
				),
				takeUntil(this.destroySubject$),
			)
			.subscribe();
	}

	onKeywordChange(newKeyword: string): void {
		console.log("search keyword changed to:", newKeyword);
		this.keywordSubject$.next(newKeyword);

		if (newKeyword) {
			this.isAfterInitialLoad = true;
		}
	}

	ngOnDestroy(): void {
		this.reposService.updateExtendedReposByKeyword(null); // reset results
		this.destroySubject$.next();
		this.destroySubject$.complete();
	}
}
