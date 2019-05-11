const weekDays = [
	'Sunday',
	'Monday',
	'Tuesday',
	'Wednesday',
	'Thursday',
	'Friday',
	'Saturday',
];

export default function addDateFunctions() {
	const datePrototype = window.Date.prototype;
	datePrototype.showTime = function () {
		const rawHour = this.getHours();
		const hour = rawHour % 12;
		return `${hour === 0 ? 12 : hour}:${('0' + this.getMinutes()).substr(-2)}${rawHour >= 12 ? 'pm' : 'am'}`;
	}
	datePrototype.showShortDay = function () {
		return `${weekDays[this.getDay()].substr(0,3)}`;
	}
	datePrototype.showDay = function () {
		return `${weekDays[this.getDay()]}`;
	}
	datePrototype.showMonthDate = function () {
		return `${('0' + (this.getMonth() + 1)).substr(-2)}/${('0' + this.getDate()).substr(-2)}`;
	}
	datePrototype.showMonthDateYear = function () {
		return this.showMonthDate() + '/' + this.getFullYear();
	}
	datePrototype.showLogString = function () {
		const logString = `${this.showTime()} ${this.showShortDay()}, ${this.showMonthDate()}`;
		const thisYear = this.getFullYear();
		if (new Date().getFullYear() !== thisYear) {
			return logString + '/' + thisYear;
		}else{
			return logString;
		}
	}
	datePrototype.showShortLogString = function () {
		const now = new Date();
		const millisecondsInSixDays = 1000 * 60 * 60 * 24 * 6;
		if (now.getDate() === this.getDate() && now.getMonth() === this.getMonth() && now.getFullYear() === this.getFullYear()) {
			return this.showTime();
		} else if (this.getTime() < new Date(now.getTime() - millisecondsInSixDays)) {
			return this.showMonthDate();
		} else {
			return this.showDay();
		}
	}
}