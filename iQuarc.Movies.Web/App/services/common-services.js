var module = angular.module('services', []);

module.service('common', function () {
	var base = '/api/movies/';

	return {
		urls: {
			latest: function (count) { return base + 'latest?count=' + count; },
			movie: function (id) { return base + id; }
		}
	};
});