'use strict';

angular.
    module('bagApp')
    .component('appRoot', {
        templateUrl: '/frontEnd/app/app-root/app-root.template.html',
        controller: ['DataCenter', function (DataCenter) {
            this.bags = DataCenter.getAllBags();
            this.cart = DataCenter.cart;

            var addProductToCartByID = function (id) {
                DataCenter.addProductToCart(id)
            }
        }]
    });