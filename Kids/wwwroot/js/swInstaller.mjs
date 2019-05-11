function cacheOnLoad() {
	if (navigator.serviceWorker.controller && navigator.serviceWorker.controller.state === 'activated') {
		const scriptURLs = Array.prototype.map.call(document.querySelectorAll('script[src]'), s => s.src);
		const styleURLs = Array.prototype.map.call(document.querySelectorAll('link[rel="stylesheet"]'), l => l.href);
		const allAssets = scriptURLs.concat(styleURLs);
		navigator.serviceWorker.controller.postMessage({
			command: 'precache',
			urls: allAssets,
		});
	}
}

export default async function swInstaller() {
	if (!navigator.serviceWorker) return;

	if (!navigator.serviceWorker.controller) {

		window.addEventListener('load', cacheOnLoad);

		await navigator.serviceWorker.register('sw.js', {
			scope: './'
		});

	}
}