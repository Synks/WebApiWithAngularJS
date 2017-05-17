// Defining angularjs module
var app = angular.module('personModule', ['ngTable']);

// Defining angularjs Controller and injecting ProductsService
app.controller('personController', function ($scope, $http, personService,ngTableParams) {
    $scope.showVal = false;
    $scope.hideVal = false;
    $scope.personsData = null;
    $scope.person = {
        PersonId: '',
        FirstName: '',
        MiddleName: '',
        LastName: '',
        PrimaryEmailAddress: '',
        SecondaryEmailAddress: '',
        PhoneNumber: '',
        MobileNumber: ''
    };
    // Fetching records from the factory created at the bottom of the script file
    personService.GetAllRecords().then(function (d) {
        $scope.personsData= d.data; // Success
    }, function () {
        alert('Error Occured !!!'); // Failed
        });
    //$scope.personsTable = new ngTableParams({ page: 1, count: 5 },
    //    {
    //        total: $scope.users.length,
    //        getData: function ($defer, params) {
    //            $scope.data = $scope.personsData.slice((params.page() - 1) * params.count(), params.page() * params.count());
    //            $defer.resolve($scope.data);
    //        }
    //    });
    $scope.edit = function (data) {
        $scope.showVal = true;
        $scope.person = {
            PersonId: data.PersonId,
            FirstName: data.FirstName,
            MiddleName: data.MiddleName,
            LastName: data.LastName,
            PrimaryEmailAddress: data.PrimaryEmailAddress,
            SecondaryEmailAddress: data.SecondaryEmailAddress,
            PhoneNumber: data.PhoneNumber,
            MobileNumber: data.MobileNumber
        };
    }
    $scope.clear = function () {
        $scope.person = {
            PersonId: '',
            FirstName: '',
            MiddleName: '',
            LastName: '',
            PrimaryEmailAddress: '',
            SecondaryEmailAddress: '',
            PhoneNumber: '',
            MobileNumber: ''
        };
    }
    $scope.add = function () {
        $scope.hideVal = true;
    }
    //Add New Person
    $scope.save = function () {
        if ($scope.person.FirstName != "" &&
            $scope.person.LastName != "" && $scope.person.MobileNumber != "") {
            // Call Http request using $.ajax

            //$.ajax({
            //    type: 'POST',
            //    contentType: 'application/json; charset=utf-8',
            //    data: JSON.stringify($scope.Product),
            //    url: 'api/Product/PostProduct',
            //    success: function (data, status) {
            //        $scope.$apply(function () {
            //            $scope.productsData.push(data);
            //            alert("Product Added Successfully !!!");
            //            $scope.clear();
            //        });
            //    },
            //    error: function (status) { }
            //});

            // or you can call Http request using $http
            $http({
                method: 'POST',
                url: 'http://localhost:64830/api/person',
                data: $scope.person
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.personsData.push(response.data);
                $scope.clear();
                $scope.hideVal = false;
                alert("Person Added Successfully !!!");
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    $scope.cancel = function () {
        $scope.clear();
    }

    // Update Person details
    $scope.update = function () {
        if ($scope.person.FirstName != "" &&
            $scope.person.MiddleName != "" && $scope.person.LastName != "") {
            $http({
                method: 'PUT',
                url: 'http://localhost:64830/api/person/' + $scope.person.PersonId,
                data: $scope.person
            }).then(function successCallback(response) {
                $scope.personsData = response.data;
                $scope.clear();
                $scope.showVal = false;
                alert("Person Updated Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Delete Person
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            url: 'http://localhost:64830/api/person' + $scope.personsData[index].PersonId,
        }).then(function successCallback(response) {
            $scope.personsData.splice(index, 1);
            alert("Person Deleted Successfully !!!");
        }, function errorCallback(response) {
            alert("Error : " + response.data.ExceptionMessage);
        });
    };

});

// Here I have created a factory which is a popular way to create and configure services.
// You may also create the factories in another script file which is best practice.

app.factory('personService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('http://localhost:64830/api/person');
    }
    return fac;
});