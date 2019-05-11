export async function getFamily() {
	const res = await fetch('/api/family/', {
		method: "POST", // *GET, POST, PUT, DELETE, etc.
		mode: "cors", // no-cors, cors, *same-origin
		cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
		credentials: "same-origin", // include, *same-origin, omit
		headers: {
			"Content-Type": "application/json; charset=utf-8",
		},
		redirect: "follow", // manual, *follow, error
		referrer: "no-referrer", // no-referrer, *client
		body: JSON.stringify({
			family: 1
		}), // body data type must match "Content-Type" header
	});
	const family = await res.json();
	return family;
};