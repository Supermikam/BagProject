'use strict';

angular.
    module('bagApp')
    .component('bagDetail', {
        templateUrl: '/frontEnd/app/bag-detail/bag-detail.template.html',
        bindings: { bag: '<' },

    });
