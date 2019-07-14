import pwaInstaller from './pwaInstaller.js';
import swInstaller from './swInstaller.js';
import addDateFunctions from './addDateFunctions.js';
import {
	updatePoints,
	getKids,
	addKidAction,
} from './kidApi.js';
import {
	getFamily,
} from './familyApi.js';
import {
	initMobileNav,
	hideMobileNav,
	showMobileNav,
} from './mobileNav.js';
import autoUpdateData from './autoUpdateData.js';
import app from './vueApp.js';
import {
	actionTarget,
} from './domElements.js';

window.hideMobileNav = hideMobileNav;
window.showMobileNav = showMobileNav;

pwaInstaller(document.getElementById('install-prompt'), document.getElementById('install-button'));
swInstaller();
addDateFunctions();

initMobileNav();

let db;
let kidStore;

if ('indexedDB' in window) {
	const request = indexedDB.open('KidsDatabase', 1);
	request.onerror = function (e) {
		console.error(e);
	};
	request.onsuccess = function (e) {
		db = e.target.result;
		db.onerror = function (ev) {
			// Generic error handler for all errors targeted at this database's
			// requests!
			// alert('Database error: ' + event.target.errorCode);
			console.log('Database error: ', ev);
		};
		if (app && app.kids) {
			if (app.kids.length > 0) {
				const kidObjectStore = db
					.transaction('kids', 'readonly')
					.objectStore('kids');
				app.kids.forEach(function (kid) {
					kidObjectStore.add(kid);
				});
			} else {
				const kidObjectStore = db
					.transaction('kids', 'readonly')
					.objectStore('kids');
				const req = kidObjectStore.getAll();
				req.onsuccess = function (ev) {
					ev.target.result.forEach(k => updateKidFromFetchData(k));
					// console.log('suc', ev);
				};
			}
		}
	};
	request.onupgradeneeded = function (e) {
		db = e.target.result;
		// Create an objectStore to hold information about our customers. We're
		// going to use 'ssn' as our key path because it's guaranteed to be
		// unique - or at least that's what I was told during the kickoff meeting.
		kidStore = db.createObjectStore('kids', {
			keyPath: 'id'
		});

		// Use transaction oncomplete to make sure the objectStore creation is
		// finished before adding data into it.
		kidStore.transaction.oncomplete = function () {
			console.log('com');
			// Store values in the newly created objectStore.
			if (app && app.kids) {
				if (app.kids.length > 0) {
					const kidObjectStore = db
						.transaction('kids', 'readwrite')
						.objectStore('kids');
					app.kids.forEach(function (kid) {
						kidObjectStore.add(kid);
					});
				} else {
					const kidObjectStore = db
						.transaction('kids', 'read')
						.objectStore('kids');
					const req = kidObjectStore.getAll();
					req.onsuccess = function (ev) {
						console.log('suc', ev);
					};
				}
			}
		};
	};
}

function updateKidFromFetchData(kid) {
	const kidIndex = app.kids.findIndex(ak => ak.id === kid.id);
	const myKid = kidIndex > -1 ? app.kids[kidIndex] : undefined;
	if (myKid) {
		if (kid.version > myKid.version) {
			for (const prop of Object.getOwnPropertyNames(kid)) {
				if (kid[prop] !== myKid[prop]) {
					myKid[prop] = kid[prop];
				}
			}
		}
	} else {
		kid.showDetail = false;
		app.kids.push(kid);
	}

	const kidObjectStore = db ?
		db.transaction('kids', 'readwrite').objectStore('kids') :
		null;
	if (kidObjectStore) {
		const request = kidObjectStore.get(kid.id);
		request.onerror = function (event) {
			console.log(event);
			// Handle errors!
		};
		request.onsuccess = function (event) {
			console.log(event);
			// // Get the old value that we want to update
			// var data = event.target.result;

			// // update the value(s) in the object that you want to change
			// data.age = 42;

			// Put this updated object back into the database.
			var requestUpdate = kidObjectStore.put(kid);
			requestUpdate.onerror = function (event) {
				// Do something with the error
			};
			requestUpdate.onsuccess = function (event) {
				// Success - the data is updated!
			};
		};
	}
}

function getKidVersions() {
	if (app.kids.length === 0) return null;
	return app.kids.map(k => { return {Id: k.id, Version: k.version} });
}

async function clickKidAction(ev) {
	const targetId = Number(actionTarget.getAttribute('data-target-id'));
	if (targetId) {
		const kid = app.kids.find(k => k.id === targetId);
		if (kid) {
			const actionId = Number(ev.target.getAttribute('data-id'));
			const description = ev.target.getAttribute('data-description');
			const points = Number(ev.target.getAttribute('data-points'));
			await addKidAction(targetId, actionId, description, points);
			hideMobileNav();
			loadKids();
		}
	}
}

const actionLITemplate = document.createElement('li');
actionLITemplate.className = 'list-group-item';
const actionLIAnchorTemplate = document.createElement('a');

function sortEvents(a, b) {
	const nameA = a.description.toLowerCase();
	const nameB = b.description.toLowerCase();
	if (nameA < nameB) {
		return -1;
	} else if (nameA > nameB) {
		return 1;
	} else {
		return 0;
	}
}

function getActionListItem(actionId, description, points) {
	const li = actionLITemplate.cloneNode(true);
	const a = actionLIAnchorTemplate.cloneNode(true);
	a.innerText = `${description} (${points})`;
	a.setAttribute('data-id', actionId);
	a.setAttribute('data-description', description);
	a.setAttribute('data-points', points);
	a.addEventListener('click', clickKidAction);
	li.appendChild(a);
	return li;
}

export async function loadFamily() {
	const family = await getFamily();
	const positiveEvents = family.events.filter(action => action.points > 0);
	const negativeEvents = family.events.filter(action => action.points <= 0);

	positiveEvents.sort(sortEvents);
	negativeEvents.sort(sortEvents);

	const positiveActionList = document.getElementById('positive-action-list');
	const negativeActionList = document.getElementById('negative-action-list');

	positiveEvents.forEach(action => {
		const li = getActionListItem(action.id, action.description, action.points);
		positiveActionList.appendChild(li);
	});

	negativeEvents.forEach(action => {
		const li = getActionListItem(action.id, action.description, action.points);
		negativeActionList.appendChild(li);
	});
};

export async function loadKids() {
	const appKids = getKidVersions();

	const kids = await getKids(appKids);
	if (kids) {
		kids.forEach(updateKidFromFetchData);
	}
};

export async function changePoints(points, kidId) {
	const success = await updatePoints(points, kidId);
	if (success) {
		loadKids();
	}
};

document.addEventListener('DOMContentLoaded', loadFamily);
document.addEventListener('DOMContentLoaded', autoUpdateData);

window.changePoints = changePoints;

document.addEventListener('DOMContentLoaded', () => {
	document.getElementById('content').style.display = '';
});