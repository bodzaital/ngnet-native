import { HttpClient } from '@angular/common/http';
import { Component, Signal, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
	selector: 'app-root',
	imports: [FormsModule],
	templateUrl: './app.html',
	styleUrl: './app.css'
})
export class App {
	protected title = signal<string>("");

	constructor(private http: HttpClient) {
		alert(location.port);
	}

	protected set() {
		this.http.post(`${location.origin}/set-title`, {
			"value": this.title()
		}).subscribe();
	}
}
