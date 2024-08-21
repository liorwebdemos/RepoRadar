import { Component, EventEmitter, Output } from "@angular/core";

@Component({
	selector: "app-search-bar",
	standalone: true,
	templateUrl: "./search-bar.component.html",
	host: { class: "z-[2]" }, // "beat" the rest of the layout's z-index, to allow outline/shadow to show properly
})
export class SearchBarComponent {
	@Output() keywordChange = new EventEmitter<string>(); // Declare the output event

	onInput(event: any): void {
		const newKeyword = event.target.value;
		this.keywordChange.emit(newKeyword);
	}
}
