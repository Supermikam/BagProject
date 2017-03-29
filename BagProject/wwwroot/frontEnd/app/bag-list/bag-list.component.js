'use strict';

angular.
    module('bagApp').
    component('bagList', {
        templateUrl: '/frontEnd/app/bag-list/bag-list.template.html',
        controller:
        function BagListController($http) {
            var self = this;
            this.bags = null;
            this.orderProp = 'productName';
     

                var init = function () {
                    $http({ method: 'GET', url: '/api/product' }).then(function (response) {
                        self.bags = response.data;
      
                    })
                };

                init();
            }
        
    });

