import { CommonModule } from "@angular/common";
import { ExtendedRepo } from "./../models/extended-repo";
import { Component, Input } from "@angular/core";
import { ReposService } from "../repos.service";

@Component({
	selector: "app-repo-box",
	standalone: true,
	imports: [CommonModule],
	templateUrl: "./repo-box.component.html",
})
export class RepoBoxComponent {
	@Input() extendedRepo: ExtendedRepo | null = null;

	constructor(public reposService: ReposService) {}
}
