'use strict';

angular.
    module('bagApp')
    .component('appRoot', {
        templateUrl: '/frontEnd/app/app-root/app-root.template.html',
        controller: ['DataCenter', '$log', function (DataCenter, $log) {
            this.bags = DataCenter.getAllBags();
            this.cart = DataCenter.cart;

            this.addProductToCartByID = function (id) {          
                var bag = DataCenter.getBagByID(id);
                DataCenter.addProductToCart(id);
            }

        }]
    });