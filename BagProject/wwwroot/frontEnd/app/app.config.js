'use strict';

angular.
    module('bagApp').
    config(['$locationProvider', '$stateProvider','$urlServiceProvider', '$logProvider',
        function config($locationProvider, $stateProvider, $urlServiceProvider,Bag,$logProvider) {
            $urlServiceProvider.rules.otherwise({ state: 'app.list' });
            $stateProvider.state('app', {
                //bags is set up in controller using DataCenter service
                url: '/app',
                component: 'appRoot',
            });

            $stateProvider.state('app.list', {
                url: '/bags',
                views: {
                    main: 'bagList',
                },
            });

            $stateProvider.state('app.detail', {
                url: '/:id',
                views: {
                    main: 'bagDetail'
                },
                resolve: {
                    // bags is not resolved in the parent state, 
                    //so it cannot be injected into the fuction.
                    //Bag is retrieved from DataCenter Service. 
               
                    bag: function (DataCenter, $transition$) {
                        let id = $transition$.params().id;
                        return DataCenter.getBagByID(id);
                    }
                },
            });

            $stateProvider.state('app.cart', {
                url: '/cart',
                views: {
                    main: 'cart'
                },
                resolve: {
                    cart: function (DataCenter) {
                        return DataCenter.cart
                    }
                },
            });

            ////try to nest detail in list and use @^.^ to point to grand parent 
            //$stateProvider.state('app.list.detail', {
            //    url: '/:id',
            //    view: {
            //        "main@^.^": { component: 'bagDetail'},
            //    },
            //    resolve: {
            //        bag: function (DataCenter, $transition$) {
            //            let id = $transition$.params().id;
            //            console.log($transition$.params().id);
            //            return DataCenter.getBagByID(id);
            //        }
            //    },
            //});
        }
    ]);
