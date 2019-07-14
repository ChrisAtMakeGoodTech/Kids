export default async function pwaInstaller(installPrompt, installButton) {
	const isInApp = window.navigator.standalone === true || window.matchMedia('(display-mode: standalone)').matches;

	if (!isInApp) {

		window.addEventListener('beforeinstallprompt', async (e) => {
			installPrompt.style.display = 'block';

			e.preventDefault();

			let deferredPrompt = e;

			installButton.addEventListener('click', async function () {
				installPrompt.style.display = 'none';
				deferredPrompt.prompt();

				await deferredPrompt.userChoice;
				deferredPrompt = null;
			});

		});

	}
}