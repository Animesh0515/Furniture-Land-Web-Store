/// <reference path="angular.min.js" />
/// <reference path="login.js" />



var app = angular.module('app', []);
app.controller('myController', function ($scope, $rootScope, $window,$http) {
    
    $scope.cartQuantity = JSON.parse($window.sessionStorage.getItem("cartItems")) ? JSON.parse($window.sessionStorage.getItem("cartItems")).length: 0;
    //login status stored in seesion so that it can be used in every page load
    if ($window.sessionStorage.getItem("LoginID") == undefined) {
        $scope.loginstatus = "Login";
    }
    else {
        if ($window.sessionStorage.getItem("LoginID") != "")
            var i = $window.sessionStorage.getItem("LoginID");
        $scope.loginstatus = "Logout";
    }
    
    //to change status after login
    $rootScope.$on("ChangeLoginStatus", function (event, parameter) {
        
        $rootScope.loginstatus = parameter.LoginService.name(parameter.username, parameter.password);

      
        
    });

    $rootScope.$on("ChangecartQuantity", function (event, cartQuantity) {
               
        $scope.cartQuantity = cartQuantity.Quantity;

      
        
    });

    var myFunction = function () {
        debugger;
    };
    $scope.LoginAction = function () {
        

        if ($scope.loginstatus == "Login") {
            $window.location.href = '/Home/Login';

        }
        else {
            $window.sessionStorage.removeItem('LoginID');
            $window.location.href = '/Home/Index';
            //$window.sessionStorage.$reset();

        }
    }
    $scope.redirect = function () {
        
        $window.location.href = '/Cart/Cart';
        //var a = JSON.parse($window.sessionStorage.getItem("cartItems"));
        //$.ajax({
        //    type: 'Get',
        //    data: { Items: a },
        //    url: "Cart/Cart",
        //    //...
        //});
        //$location.url('/#showemp/' + param);
        //$window.location.href = 'Cart/Cart?Items=' + JSON.parse($window.sessionStorage.getItem("cartItems"));

        //$http({
        //    method: "Post",
        //    url: '/Cart/Cart',
        //    data: { Items: JSON.parse($window.sessionStorage.getItem("cartItems")) }
        //});

    };
    
});
