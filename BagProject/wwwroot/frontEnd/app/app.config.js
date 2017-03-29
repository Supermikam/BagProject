'use strict';

angular.
    module('bagApp').
    config(['$locationProvider', '$routeProvider',
        function config($locationProvider, $routeProvider) {
            $locationProvider.hashPrefix('!');

            $routeProvider.
                when('/bags', {
                    template: '<bags></bags>'
                }).
                otherwise('/bags');
        }
    ]);
