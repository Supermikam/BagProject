angular.module('app.services')
    .factory('DataCenter',
    ['Bag', 'Supplier', 'Category', 'Customer', 'Order',
        function (Bag, Supplier, Category, Customer, Order) {
            var _DataCenter = {
                bags : Bag.query(),

                getAllBags : function () {
                    return this.bags;
                },

                getBagByID : function (id) {
                    return this.bags.find(function (bag) { return bag.productID == id });
                }
            }

            return _DataCenter;
          

        }]);