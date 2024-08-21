import { ReposService } from "./../repos.service";
import { Component, Input } from "@angular/core";
import { RepoBoxComponent } from "../repo-box/repo-box.component";
import { CommonModule } from "@angular/common";

@Component({
	selector: "app-favorites",
	standalone: true,
	imports: [RepoBoxComponent, CommonModule],
	templateUrl: "./favorites.component.html",
})
export class FavoritesComponent {
	@Input() public isEachItemFullWidth = false; // non-responsive items for the sidebar

	constructor(public reposService: ReposService) {}

	ngOnInit(): void {
		this.reposService.initializeFavoriteExtendedRepos().subscribe((data) => {
			console.log("initial favorites:", data);
		});
	}
}
