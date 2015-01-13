var module = angular.module('services');

module.service('promise', [
	'$q', '$http', function ($q, $http) {
		return {
			fromRequest: function (request) {
				var deffered = $q.defer();

				request
					.success(function (data) {
						deffered.resolve(data);
					})
					.error(function (data) {
						console.log(data);
						deffered.reject({ message: data });
					});

				return deffered.promise;
			},

			get: function (url, params) {
				var request = $http.get(url, params);
				return this.fromRequest(request);
			},

			post: function (url, params, options) {
				var request = $http.post(url, params, options);
				return this.fromRequest(request);
			},

			put: function (url, params) {
				var request = $http.put(url, params);
				return this.fromRequest(request);
			},

			'delete': function (url, params) {
				var request = $http.delete(url, params);
				return this.fromRequest(request);
			},

			fromData: function (data) {
				var deffered = $q.defer();
				deffered.resolve(data);
				return deffered.promise;
			},

			create: function () {
				return $q.defer();
			}
		}
	}
]);