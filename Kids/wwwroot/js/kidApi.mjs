let getKidsInProgress = false;

function getKidFetchData(appKids) {
	if ((!appKids) || appKids.length === 0) return undefined;

	return {
		method: "POST", // *GET, POST, PUT, DELETE, etc.
		mode: "cors", // no-cors, cors, *same-origin
		cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
		credentials: "same-origin", // include, *same-origin, omit
		headers: {
			"Content-Type": "application/json; charset=utf-8",
		},
		redirect: "follow", // manual, *follow, error
		referrer: "no-referrer", // no-referrer, *client
		body: JSON.stringify(appKids), // body data type must match "Content-Type" header
	};

}

export async function addKidAction(kidId, eventId, description, points) {
	const data = {
		UserId: 4,
		FamilyId: 1,
		KidId: kidId,
		EventId: eventId,
		Note: description,
		Points: points,
	};
	const response = await fetch('/api/kids/addEvent', {
		method: "POST", // *GET, POST, PUT, DELETE, etc.
		mode: "cors", // no-cors, cors, *same-origin
		cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
		credentials: "same-origin", // include, *same-origin, omit
		headers: {
			"Content-Type": "application/json; charset=utf-8",
		},
		redirect: "follow", // manual, *follow, error
		referrer: "no-referrer", // no-referrer, *client
		body: JSON.stringify(data), // body data type must match "Content-Type" header
	});

	return response.status >= 200 && response.status < 300;
}

export async function updatePoints(points, kidId) {
	const data = {
		id: kidId,
		points: points,
	};
	const response = await fetch('/api/kids/updatePoints', {
		method: "POST", // *GET, POST, PUT, DELETE, etc.
		mode: "cors", // no-cors, cors, *same-origin
		cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
		credentials: "same-origin", // include, *same-origin, omit
		headers: {
			"Content-Type": "application/json; charset=utf-8",
		},
		redirect: "follow", // manual, *follow, error
		referrer: "no-referrer", // no-referrer, *client
		body: JSON.stringify(data), // body data type must match "Content-Type" header
	});

	return response.status === 200;

}

export async function getKids(appKids) {
	if (!getKidsInProgress) {
		getKidsInProgress = true;
		try {

			const fetchData = getKidFetchData(appKids);
			const response = await fetch('/api/kids/', fetchData);
			getKidsInProgress = false;
			if (response.status === 200) {
				const kids = await response.json();
				return kids;
			} else {
				return null;
			}

		} finally {
			getKidsInProgress = false;
		}
	}
}