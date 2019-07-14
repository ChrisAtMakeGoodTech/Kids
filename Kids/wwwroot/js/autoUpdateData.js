import {
	loadKids
} from './front.js';

let hidden, visibilityChange, refreshInterval;
if (typeof document.hidden !== "undefined") { // Opera 12.10 and Firefox 18 and later support 
	hidden = "hidden";
	visibilityChange = "visibilitychange";
} else if (typeof document.msHidden !== "undefined") {
	hidden = "msHidden";
	visibilityChange = "msvisibilitychange";
} else if (typeof document.webkitHidden !== "undefined") {
	hidden = "webkitHidden";
	visibilityChange = "webkitvisibilitychange";
}

function updateLoadKidsInterval() {
	if (document[hidden]) {
		if (refreshInterval) {
			clearInterval(refreshInterval);
			refreshInterval = undefined;
		}
	} else if (!refreshInterval) {
		loadKids();
		refreshInterval = setInterval(loadKids, 60000);
	}
}

export default function autoUpdateData() {
	if (hidden && visibilityChange) {
		document.addEventListener(visibilityChange, updateLoadKidsInterval);
		updateLoadKidsInterval();
	} else {
		loadKids();
	}
};