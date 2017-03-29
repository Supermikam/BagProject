'use strict';

angular.
    module('shared.dataSource').
    factory('BagData', ['$resource',
        function ($resource) {
            return $resource('http://localhost:50209/api/product', {}, {
                query: {
                    method: 'GET',
                    params: { },
                    isArray: true
                }
            });
        }
    ]);
