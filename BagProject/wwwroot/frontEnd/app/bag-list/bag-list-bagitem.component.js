'use strict';

angular.
    module('bagApp')
    .component('bagListBagitem', {
        bindings: { bag: '<' },
        template: `
           <li>
                <a ui-sref="app.detail({id:$ctrl.bag.productID})" >
                    <img ng-src="{{$ctrl.bag.imageLink}}" alt="{{$ctrl.bag.productName}}" />
                </a>
                <a ui-sref="app.detail({id:$ctrl.bag.productID})">{{$ctrl.bag.productName}}</a>
                <p>{{$ctrl.bag.discription}}</p>
                <p>{{$ctrl.bag.price}}</p>
            </li>             
        `
    })
