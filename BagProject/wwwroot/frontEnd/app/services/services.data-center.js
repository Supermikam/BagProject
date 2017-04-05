angular.module('app.services')
    .factory('DataCenter',
    ['Bag', 'Supplier', 'Category', 'Customer', 'Order',
        function (Bag, Supplier, Category, Customer, Order) {
            var _DataCenter = {
                bags: Bag.query(),

                getAllBags: function () {
                    return this.bags;
                },

                getBagByID: function (id) {
                    return this.bags.find(function (bag) { return bag.productID == id });
                },

                cart: {
                    totalPrice: 0,
                    cartlines: [],
                },

                getCart: function () {
                    return this.cart;
                },

                addProductToCart: function (id) {
                    function CartLine(product, quantity) {
                        this.product = product;
                        this.quantity = quantity;
                    };
                    var bagToAdd = getBagByID(id);
                    if (bagToAdd !== null) {
                        var potentialLine = this.cart.cartlines.find(line => line.product.ProductID == id);
                        if (potentialLine !== null) {
                            potentialLine.quantity += 1;
                            this.cart.totalPrice += potentialLine.product.price;
                        } else {
                            var newLine = new CartLine(bagToAdd, 1);
                            this.cart.cartlines.push(newLine);
                            this.cart.totalPrice += newLine.product.price;
                        }
                    }
                },

                removeProductFromCart: function (id) {
                    var bagToRemove = getBagByID(id);
                    if (bagToRemove !== null) {
                        var potentialLine = this.cart.cartlines.find(line => line.product.productID == id);
                        if (potentialLine !== null) {
                            this.cart.totalPrice -= potentialLine.product.price;
                            if (potentialLine.quantity <= 1) {
                                this.cart.cartlines.remove(line => line.product.ProductID == id);
                            } else {
                                potentialLine.quantity -= 1;
                            }
                        }
                    }
                },
            };

            return _DataCenter;
          

        }]);