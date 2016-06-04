(function (app) {
    //debugger;
    var regionCtrl = function ($scope, $log, $routeParams, regionService, ngToast) {

        regionService.getAll()
                        .success(function (mod) {
                            $scope.model = mod;
                            //$scope.model.Mode = "Add";
                        });
        
        $scope.edit = function (region) {
            debugger;
            $scope.model.Region = angular.copy(region);
            $scope.model.Mode = "Edit";
            angular.element('#regionCode').focus();
        };

        
        $scope.cancel = function () {
            debugger;
            regionService.getBlank().success(function (model) {
                debugger;
                $scope.model.Address = model;
            });
        };

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updateRegion();
            } else {
                creatRegion();
            }
        };

        var creatRegion = function () {
            debugger;
            regionService.create($scope.model).success(function (mod) {
                debugger;
                if (mod.IsValid) {
                    $scope.model = mod;
                }
                alert(mod.Message);
                angular.element('#' + mod.FieldId).focus();
            }).error(function (err) {
                //$window.alert("Error: " + err.ExceptionMessage);
                debugger;
                alert("Error:" + err.ExceptionMessage);
            });
        };

        var updateRegion = function () {
            debugger;
            regionService.update($scope.model.Region).success(function (mod) {
                if (mod.IsValid) {
                    debugger;
                    $scope.model = mod;
                   // $scope.model.Mode = "Add";
                }
                alert(mod.Message);
                angular.element('#' + mod.FieldId).focus();
            }).error(function (err) {
                //$window.alert("Error: " + err.ExceptionMessage);
                debugger;
                alert("Error:" + err.ExceptionMessage);
            });
        };

        $scope.delete = function (model) {
            if (window.confirm("Are you sure, you want to delete the record?")) {
                debugger;
                model.IsActive = false;
                regionService.update(model)
                    .success(function (mod) {
                        alert(mod.Message);
                        $scope.model = mod;
                        angular.element('#' + mod.FieldId).focus();
                    });
            }
        };
    };

    regionCtrl.$inject = ["$scope", "$log", "$routeParams", "regionService", "ngToast"];
    app.controller("regionCtrl", regionCtrl);

}(angular.module("QST")));