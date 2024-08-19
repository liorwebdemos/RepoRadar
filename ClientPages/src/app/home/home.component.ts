import { Component } from "@angular/core";
import { HeaderComponent } from "../header/header.component";
import { CommonModule } from "@angular/common";

@Component({
	selector: "app-home",
	standalone: true,
	imports: [HeaderComponent, CommonModule],
	templateUrl: "./home.component.html",
	styleUrl: "./home.component.scss",
	host: { class: "flex-grow flex flex-col overflow-auto" },
})
export class HomeComponent {}
