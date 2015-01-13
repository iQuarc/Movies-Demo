var app = angular.module('app');

app.controller('movieController', [
	'$scope', '$routeParams', 'promise', 'common',
	function ($scope, $routeParams, promise, common) {
		var id = $routeParams.movieId;

		promise.get(common.urls.movie(id))
			.then(function (data) {
				$scope.entity = data;
			});
	}
]);