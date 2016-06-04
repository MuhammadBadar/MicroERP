(function (app) {
    //debugger;
    var managerCtrl = function ($scope, $log, $routeParams, managerService, areaService, cityService, ngToast) {

        managerService.getAll().success(function (mod) {
            $scope.model = mod;
        });

        $scope.edit = function (manager) {
            debugger;
            var manager = angular.copy(manager);
            cityService.getByRegion(manager.RegionCode).success(function (model) {
                debugger;
                $scope.model.Cities = model;
            });
            areaService.getByCity(manager.CityCode).success(function (model) {
                debugger;
                $scope.model.Areas = model;
            });
            managerService.getById(manager.ID).success(function (model) {
                debugger;
                $scope.model.Manager = model.Manager;
                $scope.model.Address = model.Address;
                $scope.model.Contact = model.Contact;
            });
            //$scope.model.Manager = manager; //angular.copy(manager);
            $scope.model.Mode = "Edit";
            angular.element('#managerCode').focus();
        };

        $scope.cancel = function () {
            debugger;
            managerService.getBlank().success(function (model) {
                debugger;
                $scope.mode = model;
            });
        };

        $scope.LoadCities = function (regionCode) {
            debugger;
            cityService.getByRegion(regionCode).success(function (model) {
                debugger;
                $scope.model.Cities = model;
                if (model.length == 0)
                    $scope.model.Manager.CityCode = '';
                $scope.model.Address.AreaCode = '';

            });
        }

        $scope.LoadAreas = function (cityCode) {
            debugger;
            areaService.getByCity(cityCode).success(function (model) {
                debugger;
                $scope.model.Areas = model;
                if (model.length == 0)
                    $scope.model.Manager.AreaCode = '';
            });
        }

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updateManager();
            } else {
                creatManager();
            }
        };

        var creatManager = function () {
            debugger;
            managerService.create($scope.model).success(function (mod) {
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

        var updateManager = function () {
            debugger;
            managerService.update($scope.model).success(function (mod) {
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

        $scope.delete = function (manager) {
            if (window.confirm("Are you sure, you want to delete the record?")) {
                debugger;
                $scope.model.Manager = manager;
                $scope.model.IsActive = false;
                managerService.update($scope.model).success(function (mod) {
                    if (mod.IsValid) {
                        debugger;
                        $scope.model = mod;
                    }
                        alert(mod.Message);
                        angular.element('#' + mod.FieldId).focus();
                    });
            }
        };
    };

    managerCtrl.$inject = ["$scope", "$log", "$routeParams", "managerService", "areaService", "cityService", "ngToast"];
    app.controller("managerCtrl", managerCtrl);

}(angular.module("QST")));