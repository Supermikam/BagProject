angular.module('app.services', ['ngResource'])
    .factory('Bag', function ($resource) {
        return $resource('/api/product:id', { id: '@productID' }, {
            update: {
                method: 'PUT'
            }
        });
    });