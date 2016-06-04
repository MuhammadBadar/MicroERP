(function (app) {
    //debugger;
    var areaCtrl = function ($scope, $log, $routeParams, areaService, cityService, ngToast) {

        areaService.getAll().success(function (mod) { 
                                $scope.model = mod;
                            });

        $scope.edit = function (area) {
            debugger;
            var area = angular.copy(area);
            cityService.getByRegion(area.RegionCode).success(function (model) {
                debugger;
                $scope.model.Cities = model;
            });
            $scope.model.Area = area; //angular.copy(area);
            $scope.model.Mode = "Edit";
            angular.element('#areaCode').focus();
        };

        $scope.cancel = function () {
            debugger;
            areaService.getBlank().success(function (model) {
                debugger;
                $scope.model.Address = model;
            });
        };

        $scope.LoadCities = function (regionCode) {
            debugger;
            cityService.getByRegion(regionCode).success(function (model) {
                debugger;
                $scope.model.Cities = model;
                if(model.length == 0)
                 $scope.model.Area.CityCode = '';
            });
        }

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updateArea();
            } else {
                creatArea();
            }
        };

        var creatArea = function () {
            debugger;
            areaService.create($scope.model).success(function (mod) {
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

        var updateArea = function () {
            debugger;
            areaService.update($scope.model.Area).success(function (mod) {
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
                areaService.update(model)
                    .success(function (mod) {
                        alert(mod.Message);
                        $scope.model = mod;
                        angular.element('#' + mod.FieldId).focus();
                    });
            }
        };
    };

    areaCtrl.$inject = ["$scope", "$log", "$routeParams", "areaService", "cityService", "ngToast"];
    app.controller("areaCtrl", areaCtrl);

}(angular.module("QST")));