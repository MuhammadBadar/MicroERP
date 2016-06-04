(function (app) {
    //debugger;
    var groceryKitCtrl = function ($scope, $log, $routeParams, groceryKitService, areaService, cityService, ngToast) {

        groceryKitService.get().success(function (mod) {
            $scope.model = mod;
            debugger;
            $scope.model.TotalIncome = $scope.model.Salary + $scope.model.Donation + $scope.model.OtherIncome;
            $scope.model.TotalCost = $scope.model.FoodCost + $scope.model.HouseRent + $scope.model.SchoolCost + $scope.model.UtilitiesCost + $scope.model.MedicalCost;
            $scope.model.ShortFallInCash = $scope.model.TotalIncome + $scope.model.TotalCost;
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
            groceryKitService.getById(manager.ID).success(function (model) {
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
            groceryKitService.getBlank().success(function (model) {
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
                updateGroceryKit();
            } else {
                createGroceryKit();
            }
        };

        var createGroceryKit = function () {
            debugger;
            groceryKitService.create($scope.model).success(function (mod) {
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

        var updateGroceryKit = function () {
            debugger;
            groceryKitService.update($scope.model).success(function (mod) {
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
                groceryKitService.update($scope.model).success(function (mod) {
                    if (mod.IsValid) {
                        debugger;
                        $scope.model = mod;
                    }
                    alert(mod.Message);
                    angular.element('#' + mod.FieldId).focus();
                });
            }
        };

        // DatePicker
        $scope.minDate = new Date();
        $scope.regDateStatus = {
            opened: false
        };
        $scope.dobOpen = {
            opened: false
        };
        $scope.regDateOpen = function ($event) {
            debugger;
            $event.preventDefault();
            $event.stopPropagation();
            $scope.regDateStatus.opened = true;
        };

        $scope.dobOpen = function ($event) {
            debugger;
            $event.preventDefault();
            $event.stopPropagation();
            $scope.dobOpen.opened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yyyy',
            startingDay: 1,
            showWeeks: 'false'
        };

        $scope.formats = ['MM/dd/yyyy', 'dd/MM/yyyy', 'dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[1];

        $scope.status = {
            opened: false
        };

        //End DatePicker
    };

    groceryKitCtrl.$inject = ["$scope", "$log", "$routeParams", "groceryKitService", "areaService", "cityService", "ngToast"];
    app.controller("groceryKitCtrl", groceryKitCtrl);

}(angular.module("QST")));