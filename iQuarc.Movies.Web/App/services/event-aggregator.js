var module = angular.module('services');

module.service('eventAggregator', function () {
	var handlers = {};

	var service = {
		subscribe: subscribe,
		unsubscribe: unsubscribe,
		publish: publish
	};

	return service;

	function subscribe(eventName, callback) {
		var callbacks = handlers[eventName] || (handlers[eventName] = []);
		callbacks.push(callback);
		console.log('Subscribed from event "' + eventName + '"');
	}

	function unsubscribe(eventName, callback) {
		var callbacks = handlers[eventName];
		if (!callbacks)
			return;

		for (var i = callbacks.length - 1; i >= 0; --i) {
			if (callbacks[i] === callback) {
				callbacks.splice(i, 1);
				console.log('Unsubscribed from event "' + eventName + '"');
			}
		}

		if (callbacks.length === 0) {
			delete handlers[eventName];
		}
	}

	function publish(eventName, args) {
		console.log('Publish event "' + eventName + '": ' + args);
		var callbacks = handlers[eventName];
		if (callbacks) {
			callbacks.forEach(function (handler) {
				handler(args);
			});
		}
	}
});