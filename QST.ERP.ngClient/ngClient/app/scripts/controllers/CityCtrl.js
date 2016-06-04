(function (app) {
    //debugger;
    var cityCtrl = function ($scope, $log, $routeParams, cityService, ngToast) {

        cityService.getAll()
                            .success(function (mod) {
                                $scope.model = mod;
                            });

        $scope.edit = function (city) {
            debugger;
            $scope.model.City = angular.copy(city);
            $scope.model.Mode = "Edit";
            angular.element('#cityCode').focus();
        };

        $scope.cancel = function () {
            debugger;
            cityService.getBlank().success(function (model) {
                debugger;
                $scope.model.Address = model;
            });
        };

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updateCity();
            } else {
                creatCity();
            }
        };

        var creatCity = function () {
            debugger;
            cityService.create($scope.model).success(function (mod) {
                if (mod.IsValid) {
                    debugger;
                    $scope.model = mod;
                }
                
                alert(mod.Message);
                angular.element('#' + mod.FieldId).focus();
            }).error(function (err) {
                alert("Error:" + err.ExceptionMessage);
            });
        };

        var updateCity = function () {
            debugger;
            cityService.update($scope.model.City).success(function (mod) {
                if (mod.IsValid) {
                    debugger;
                    $scope.model = mod;
                }
                alert(mod.Message);
                angular.element('#' + mod.FieldId).focus();

            }).error(function (err) {
                alert("Error:" + err.ExceptionMessage);
            });
        };

        $scope.delete = function (model) {
            if (window.confirm("Are you sure, you want to delete the record?")) {
                debugger;
                model.IsActive = false;
                cityService.update(model)
                    .success(function (mod) {
                        alert(mod.Message);
                        $scope.model = mod;
                        angular.element('#' + mod.FieldId).focus();
                    });
            }
        };
    };

    cityCtrl.$inject = ["$scope", "$log", "$routeParams", "cityService", "ngToast"];
    app.controller("cityCtrl", cityCtrl);

}(angular.module("QST")));