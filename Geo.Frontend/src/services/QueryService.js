import {ReplaySubject} from "rxjs";
import {switchMap, takeUntil} from "rxjs/operators";
import {fromFetch} from "rxjs/fetch";
import {fromPromise} from "rxjs/internal-compatibility";

class FetchAsync {

	_destroy = new ReplaySubject(1);

	get(url) {
		return fromFetch(url)
			.pipe(
				switchMap((response) => response.json()),
				takeUntil(this._destroy)
			);
	}

	post(url, data) {
		const body = {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(data)
		}

		return fromPromise(fetch(url, body))
			.pipe(
				switchMap((response) => response.json()),
				takeUntil(this._destroy)
			);
	}
}

export default function QueryService() {
	return new FetchAsync();
}