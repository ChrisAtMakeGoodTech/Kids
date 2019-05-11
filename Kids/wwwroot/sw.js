'use strict';
//This is the service worker with the Cache-first network

const cacheName = 'kids-cache';
const precacheFiles = [
	'/',
	'/index.html',
	'/css/style.css',
];

self.addEventListener('message', function (ev) {
	switch (ev.data.command) {
		case 'precache':
			ev.waitUntil(cacheFiles(ev.data.urls));
			break;
		default:
			return;
	}
});

self.addEventListener('install', function (ev) {
	ev.waitUntil(precache().then(function () {
		return self.skipWaiting();
	}));
});

self.addEventListener('activate', function (ev) {
	return self.clients.claim();
});

self.addEventListener('fetch', function (ev) {
	if (isCacheableUrl(ev.request.url)) {
		ev.respondWith(fromServer(ev.request).catch(() => fromCache(ev.request, ev)));
		// ev.respondWith(fromCache(ev.request, ev).catch(() => fromServer(ev.request)));
	} else {
		ev.respondWith(fromServer(ev.request));
	}
});

function isCacheableUrl(url) {
	return !(url.includes('/api') || url.includes('chrome-extension://'));
}

async function cacheFiles(files) {
	const cache = await caches.open(cacheName);
	return cache.addAll(files);
}

async function precache() {
	return cacheFiles(precacheFiles);
}

async function fromCache(request, ev) {
	//we pull files from the cache first thing so we can show them fast

	const cache = await caches.open(cacheName);
	const response = await cache.match(request);

	if (response) {
		if (ev) {
			ev.waitUntil(update(request));
		}
		return response;
	} else {
		return Promise.reject('no-match');
	}

}

async function update(request) {
	//this is where we call the server to get the newest version of the 
	//file to use the next time we show view

	const cache = await caches.open(cacheName);
	const response = await fetch(request);

	if (response.ok) {
		return cache.put(request, response);
	}

}

async function fromServer(request) {
	//this is the fallback if it is not in the cache to go to the server and get it
	const requestClone = request.clone();
	const response = await fetch(request);
	if (response.ok && isCacheableUrl(request.url)) {
		const responseClone = response.clone();
		caches.open(cacheName).then(cache => cache.put(requestClone, responseClone));
	}
	return response;
}