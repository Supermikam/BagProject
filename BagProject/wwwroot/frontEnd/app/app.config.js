'use strict';

angular.
    module('bagApp').
    config(['$locationProvider', '$routeProvider',
        function config($locationProvider, $routeProvider) {
            $locationProvider.hashPrefix('!');

            $routeProvider.
                when('/bags', {
                    template: '<bag-list></bag-list>'
                })
                .when('/bags/:id', {
                    template: '<bag-detail></bag-detail>'
                })
                .otherwise('/bags');
        }
    ]);
