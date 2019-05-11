export default function afterAnimationFrame(cb) {
	return requestAnimationFrame(() => {
		setTimeout(() => {
			cb();
		}, 0);
	});
};