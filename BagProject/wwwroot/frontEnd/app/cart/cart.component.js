'use strict';

angular.
    module('bagApp').
    component('cart', {
        bindings: {
            cart: '<',
            onAddToCart: '&',
            onRemoveFromCart: '&',
            onOrder: '&',
        },
        templateUrl: '/frontEnd/app/cart/cart.template.html',

    })