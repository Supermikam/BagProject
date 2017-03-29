angular.module('bagApp')
    .controller('BagListController', function BagListController($http, $scope, BagData) {
        $scope.bags = [
            {
                "productID": 7,
                "productName": "ballet from above",
                "price": 19,
                "discription": "Designed by Laura Zalenga",
                "imageLink": "images/products/product1.jpg",
                "supplierID": 11,
                "supplier": null,
                "categoryID": 21,
                "category": null,
                "orderLines": null
            },
            {
                "productID": 8,
                "productName": "The Raven",
                "price": 19,
                "discription": "Designed by Jamie Stryker",
                "imageLink": "images/products/product2.jpg",
                "supplierID": 12,
                "supplier": null,
                "categoryID": 22,
                "category": null,
                "orderLines": null
            }
        ];
        $scope.orderProp = null;

        var init = function() {
            $http({ method: 'GET', url: '/api/product' }).then(function (response) {
                $scope.bags = response.data;
                $scope.orderProp = 'productName';
                console.log($scope.bags);
            })
        };

        init();
        console.log($scope.bags);
        
    })