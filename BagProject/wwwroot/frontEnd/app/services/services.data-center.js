angular.module('app.services')
    .factory('DataCenter',
    ['Bag', 'Supplier', 'Category', 'Customer', 'Order','$log',
        function (Bag, Supplier, Category, Customer, Order,$log) {
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
                    itemCount: 0,
                },

                addProductToCart: function (id) {
                    
                    function CartLine(product, quantity) {
                        this.product = product;
                        this.quantity = quantity;
                    };
                    var bagToAdd = this.getBagByID(id);
        
                    if (bagToAdd !== undefined) {
                        var potentialLine = this.cart.cartlines.find(line => line.product.productID == id);
                        if (potentialLine !== undefined) {
                        
                            potentialLine.quantity += 1;
                            this.cart.totalPrice += potentialLine.product.price;
                            this.cart.itemCount += 1;
                            
                        } else {
                            var newLine = new CartLine(bagToAdd, 1);
                           
                            this.cart.cartlines.push(newLine);
                            this.cart.totalPrice += newLine.product.price;
                            this.cart.itemCount += 1;
                            
                        }
                    }
                },

                removeProductFromCart: function (id) {
                    var bagToRemove = this.getBagByID(id);
                    if (bagToRemove !== null) {
                        var potentialLine = this.cart.cartlines.find(line => line.product.productID == id);
                        if (potentialLine !== null) {
                            this.cart.totalPrice -= potentialLine.product.price;
                            this.cart.itemCount -= 1;
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