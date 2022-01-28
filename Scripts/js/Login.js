
app.controller("logincontroller", function ($scope, $http, $rootScope, $window, LoginService) {
    
    $scope.login = function () {
        
        if ($scope.myForm.$valid) {
            
            $rootScope.$emit("ChangeLoginStatus", { LoginService: LoginService, username: $scope.username, password: $scope.password });


        }
        else {
            
            alert("Enter your username and password");
        }
       

    }

    
});

//Service to check the Credential
app.factory('LoginService', function ($http, $rootScope, $window) {
    
  
    return {
        name:  async function (username, password) {
            
            $http({
                method: "Post",
                url: '/Login/LogIn',
                data: { Username: username, Password: password }
            }).then(await function (result) {
                
                 if (result.data !="") {
                     $window.sessionStorage.setItem("LoginID", result.data);
                    alert("Login Succesful");
                $window.location.href = '/Home/Index';

                    

                }
                else {
                    alert("Wrong Credential");

                }
                


            })}
    };
});



