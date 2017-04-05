'use strict';

angular.
    module('bagApp')
    .component('cartIcon', {
        template: `
            <div><p><span class="glyphicon glyphicon-shopping-cart"></span><span>{{$ctrl.cartCount}}</span></p><div>
        `,
        bindings:{
            cartCount: '<'
        }

    });