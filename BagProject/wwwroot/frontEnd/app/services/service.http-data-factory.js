angular.module('app.services')
    .factory('Bag', function ($resource) {
        return $resource('/api/product:id', { id: '@productID' }, {
            update: {
                method: 'PUT'
            }
        });
    })
    .factory('Supplier', function ($resource) {
        return $resource('/api/supplier:id', { id: '@supplierID' }, {
            update: {
                method: 'PUT'
            }
        });
    })
    .factory('Category', function ($resource) {
        return $resource('/api/category:id', { id: '@categoryID' }, {
            update: {
                method: 'PUT'
            }
        });
    })
    .factory('Order', function ($resource) {
        return $resource('api/order:id', { id: '@orderID' }, {
            update: {
                method: 'PUT'
            }
        });
    })
    .factory('Customer', function ($resource) {
        return $resource('api/customer:id', { id: '@customerID' }, {
            update: {
                method: 'PUT'
            }
        });
    });