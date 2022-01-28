app.controller("CartController", function ($scope, $window, $rootScope,$http) {
    
    $scope.init = function () {
       
        $scope.cartitem = JSON.parse($window.sessionStorage.getItem("cartItems"));
        $scope.Show =true;
        if (JSON.parse($window.sessionStorage.getItem("cartItems")) == null) {

            $scope.Show = false;
        }
        else if (JSON.parse($window.sessionStorage.getItem("cartItems")).length == 0) {
            $scope.Show = false;

        }
    };

    $scope.decrement = function (index) {
        
        $scope.index = index;
        var items = JSON.parse($window.sessionStorage.getItem("cartItems"));
        angular.forEach(items, function (value, key) {
            
            if (value.Item["InventoryID"] == $scope.index ) {
                value.Quantity = value.Quantity - 1;
                if (value.Quantity == 0) {
                    var index = items.indexOf(value);
                    items.splice(index, 1);
                    
                }

            }

        });
        
        $window.sessionStorage.setItem("cartItems", JSON.stringify(items));
        $scope.cartitem = JSON.parse($window.sessionStorage.getItem("cartItems"));
        var Quantity = JSON.parse($window.sessionStorage.getItem("cartItems")).length;
        $rootScope.$emit("ChangecartQuantity", { Quantity });
        $window.location.reload();
        //$scope.init();
       
    }

    $scope.increment = function (index) {
        debugger;
        var items = JSON.parse($window.sessionStorage.getItem("cartItems"));
    angular.forEach(items, function (value, key) {
        
        if (value.Item["Quantity"] == index) {
            if (value.Item["InventoryID"] < value.Quantity)
                value.Quantity = value.Quantity + 1;

        }
        else {
            alert("Item out of stock");
        }

    });
        
        $window.sessionStorage.setItem("cartItems", JSON.stringify(items));
        $scope.cartitem = JSON.parse($window.sessionStorage.getItem("cartItems"));


    }

    $scope.deleteItem = function (index) {
        debugger;
        var cartItems = JSON.parse($window.sessionStorage.getItem("cartItems"));
        cartItems.splice(index, 1);
        $window.sessionStorage.setItem("cartItems", JSON.stringify(cartItems));
        $window.location.reload();


    };


    $scope.checkout = function () {
        if ($window.sessionStorage.getItem("LoginID") == undefined) {
            alert("Please Login to proceed further!");
            $window.location.href = "/Home/Login";
        }
    };


    $scope.placeOrder = function () {
       
        $http({
            method: "Post",
            url: "/Order/PlaceOrder",
            data: {
                itemOrdered: JSON.parse($window.sessionStorage.getItem("cartItems")), userId: $window.sessionStorage.getItem("LoginID")
            }
        }).then(function (result) {
            
            if (result.data) {
                $window.sessionStorage.removeItem("cartItems");
                $scope.cartitem = JSON.parse($window.sessionStorage.getItem("cartItems"));
                alert("SuccessFull");
                $window.location.reload();

            }
            else {
                alert("Something went wrong! Please try again.")
            }

        });

    };

});