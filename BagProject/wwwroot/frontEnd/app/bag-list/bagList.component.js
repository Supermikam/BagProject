'use strict';

angular.
    module('bagList').
    component('bags', {
        templateUrl: '/frontEnd/app/bag-list/bag-list.template.html',
        controller: ['BagList',
            function BagListController(BagData) {
                this.bags = BagData.query();
                this.orderProp = 'ProductName';
            }
        ]
    });
