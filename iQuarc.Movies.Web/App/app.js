var app = angular.module('app', ['ngRoute', 'services']);
app.constant('routes', [
	{
		url: '/',
		config: {
			templateUrl: '/app/views/home.html',
		}
	}
	,
	{
		url: '/movies/:movieId',
		config: { templateUrl: '/app/views/movie.html' }
	}
]);

app.config([
	'$routeProvider', 'routes', function ($routeProvider, routes) {
		routes.forEach(function (route) {
			$routeProvider.when(route.url, route.config);
		});
		$routeProvider.otherwise({ redirectTo: '/' });
	}
]);

app.run(function() {
	console.log('app.run');
});
