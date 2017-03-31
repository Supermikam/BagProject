'use strict';

angular.
    module('bagApp')
    .component('bagDetail', {
        templateUrl: '/frontEnd/app/bag-detail/bag-detail.template.html',
        controller: ['DataCenter', '$routeParams',
            function BagDetailController(DataCenter, $routeParams) {
                var self = this;
                this.bag = DataCenter.getBagByID($routeParams.id);
            }]
    });
