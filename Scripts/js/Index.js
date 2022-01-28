/// <reference path="angular.min.js" />
/// <reference path="layout.js" />





app.controller("IndexController", function ($scope, $timeout, $http, $window, $rootScope) {
    //Config for Slider
        var slidesInSlideshow = 3;
        var slidesTimeIntervalInMs = 3000;

        $scope.slideshow = 1;
        var slideTimer =
            $timeout(function interval() {
                $scope.slideshow = ($scope.slideshow % slidesInSlideshow) + 1;
                slideTimer = $timeout(interval, slidesTimeIntervalInMs);
            }, slidesTimeIntervalInMs);

    //To fetch images from a file and get products from database
    $scope.init = function () {
        
        var getimage = function () {
            
            $http({
                method: "Get",
                url: '/Index/GetPhotos',
                dataType: 'json',
            }).then(function (result) {

                if (result.data != "") {
                    $scope.photo1 = result.data[0].ImageName;
                    $scope.photo2 = result.data[1].ImageName;
                    $scope.photo3 = result.data[2].ImageName;
                                        
                }




            })
        };

        var getproducts = function () {
            
            $http({
                method: "Get",
                url: '/Index/GetProducts',
                dataType: 'json',
            }).then(function (result) {
                
                 $scope.products = result.data;
            })
        };


        $scope.addtocart = function (product) {
            
            var itemfound = false;
            //Checking if cart is empty
            if ($window.sessionStorage.getItem("cartItems") == undefined) {
                
                var cartitem = [{ Item: product, Quantity: 1 }];
                $window.sessionStorage.setItem("cartItems", JSON.stringify(cartitem));
            }
            else {
                //storing the cart item and searching for the item.If item is already in the cart then quantity is increased or else item is added
                var cartitem = JSON.parse($window.sessionStorage.getItem("cartItems")); 
                angular.forEach(cartitem, function (value, key) {
                    
                    if (value.Item["InventoryID"] == product.InventoryID) {
                        value.Quantity = value.Quantity + 1;
                        itemfound = true;
                    }
                    
                });
                
                if (! itemfound) {
                    cartitem.push({ Item: product, Quantity: 1 });
                }

                $window.sessionStorage.setItem("cartItems", JSON.stringify(cartitem));

                
                
            }
            
            var Quantity = JSON.parse($window.sessionStorage.getItem("cartItems")).length;
            $rootScope.$emit("ChangecartQuantity", { Quantity });
            alert("Item added to cart");

        };
        getimage();
        getproducts();
    
    }
    
});