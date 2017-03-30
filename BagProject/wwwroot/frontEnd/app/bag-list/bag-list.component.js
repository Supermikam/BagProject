'use strict';

angular.
    module('bagApp').
    component('bagList', {
        templateUrl: '/frontEnd/app/bag-list/bag-list.template.html',
        controller:
        function BagListController(Bag) {
            var self = this;
            this.bags = null;
            this.orderProp = 'productName';

            var init = function () {
                self.bags = Bag.query();
            };

            init();
        }
        
    });

