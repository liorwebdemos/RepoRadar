import { ReposService } from "./../repos.service";
import { Component } from "@angular/core";
import { HeaderComponent } from "../header/header.component";
import { CommonModule } from "@angular/common";
import {
	debounceTime,
	Observable,
	Subject,
	Subscription,
	switchMap,
	takeUntil,
} from "rxjs";
import { ExtendedRepo } from "../models/extended-repo";

@Component({
	selector: "app-home",
	standalone: true,
	imports: [HeaderComponent, CommonModule],
	templateUrl: "./home.component.html",
	styleUrl: "./home.component.scss",
	host: { class: "flex-grow flex flex-col overflow-auto" },
})
export class HomeComponent {
	private destroySubject$ = new Subject<void>();
	private keywordSubject$ = new Subject<string>();
	public keyword$: Observable<string> = this.keywordSubject$.asObservable();

	// private subscriptions: Subscription[] = [];
	// public filteredRepos: ExtendedRepo[] = [];
	// public favoriteRepos: ExtendedRepo[] = [];

	constructor(public reposService: ReposService) {}

	ngOnInit(): void {
		this.keywordSubject$
			.pipe(
				debounceTime(500),
				switchMap((value: string) =>
					this.reposService.getExtendedReposByKeyword(value),
				),
				takeUntil(this.destroySubject$),
			)
			.subscribe((repos: ExtendedRepo[]) => {
				console.log("Repos:", repos);
			});
	}

	onInput(event: any): void {
		this.keywordSubject$.next(event.target.value);
	}

	public setRepoFavorite(fullName: string): void {
		this.reposService
			.setFavoriteByFullName(fullName)
			.pipe(takeUntil(this.destroySubject$))
			.subscribe((repos: ExtendedRepo) => {
				console.log("Favorite added:", repos);
			});
	}

	ngOnDestroy() {
		this.destroySubject$.next();
		this.destroySubject$.complete();
	}
}
