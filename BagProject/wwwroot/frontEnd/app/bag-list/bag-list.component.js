'use strict';

angular.
    module('bagApp').
    component('bagList', {
        bindings: {
            bags: '<',
            onAddToCart: '&',
        },
        templateUrl: '/frontEnd/app/bag-list/bag-list.template.html'


    });



