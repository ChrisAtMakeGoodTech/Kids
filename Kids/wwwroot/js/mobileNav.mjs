import afterAnimationFrame from './afterAnimationFrame.mjs';
import app from './vueApp.mjs';

import {
	toggleMenuButton,
	mobileNavOverlay,
	mobileNavWrapper,
	actionTarget,
} from './domElements.mjs';

export function showMobileNav(targetId) {
	if (targetId && typeof targetId !== 'object') {
		actionTarget.setAttribute('data-target-id', targetId);
		const numberId = Number(targetId);
		const kid = app.kids.find(k => k.id === numberId);
		if (kid) {
			actionTarget.innerText = 'Add action for ' + kid.name;
		}
	} else {
		actionTarget.removeAttribute('data-target-id');
	}
	mobileNavWrapper.classList.add('d-block');
	afterAnimationFrame(() => {
		mobileNavWrapper.classList.add('open-nav');
	});
}

export function hideMobileNav() {
	mobileNavWrapper.classList.remove('open-nav');
	mobileNavWrapper.addEventListener('transitionend', () => {
		mobileNavWrapper.classList.remove('d-block');
	}, {
		once: true
	});
}

export function initMobileNav() {

	toggleMenuButton.addEventListener('click', showMobileNav);
	mobileNavOverlay.addEventListener('click', hideMobileNav);

}