(function () {
	if (!Array.prototype.find) {
		Array.prototype.find = function (predicate) {
			if (typeof predicate !== 'function') {
				throw new TypeError('predicate must be a function');
			}
			var list = Object(this);
			var length = list.length >>> 0;
			var thisArg = arguments[1];
			var value;

			for (var i = 0; i < length; i++) {
				value = list[i];
				if (predicate.call(thisArg, value, i, list)) {
					return value;
				}
			}
			return undefined;
		};
	}

	if (!Array.prototype.findIndex) {
		Array.prototype.findIndex = function (predicate) {
			if (typeof predicate !== 'function') {
				throw new TypeError('predicate must be a function');
			}
			var list = Object(this);
			var length = list.length >>> 0;
			var thisArg = arguments[1];
			var value;

			for (var i = 0; i < length; i++) {
				value = list[i];
				if (predicate.call(thisArg, value, i, list)) {
					return i;
				}
			}
			return -1;
		};
	}

	if (!Array.prototype.forEach) {
		Array.prototype.forEach = function (fun) {
			var len = this.length;
			if (typeof fun != "function")
				throw new TypeError('parameter should be a function');

			var thisp = arguments[1];
			for (var i = 0; i < len; i++) {
				if (i in this)
					fun.call(thisp, this[i], i, this);
			}
		};
	}

	if (!Array.prototype.where) {
		Array.prototype.where = function (predicate) {
			if (typeof predicate !== 'function') {
				throw new TypeError('predicate must be a function');
			}
			var list = Object(this);
			var length = list.length >>> 0;
			var thisArg = arguments[1];
			var value;

			var results = [];
			for (var i = 0; i < length; i++) {
				value = list[i];
				if (predicate.call(thisArg, value, i, list)) {
					results.push(value);
				}
			}
			return results;
		};
	}
})();