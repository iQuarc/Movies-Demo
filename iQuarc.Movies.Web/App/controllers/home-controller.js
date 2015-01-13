var app = angular.module('app');

app.controller('homeController', [
	'$scope', 'promise', 'common',
	function ($scope, promise, common) {
		$scope.movies = [];

		promise.get(common.urls.latest(5))
			.then(function(data) {
				$scope.movies = data;
			});
	}
]);