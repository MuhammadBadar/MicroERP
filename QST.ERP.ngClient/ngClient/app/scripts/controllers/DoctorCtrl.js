(function (app) {
    //debugger;
    var doctorCtrl = function ($scope, $log, $routeParams, doctorService, areaService, cityService, ngToast) {

        doctorService.getAll().success(function (mod) {
            $scope.model = mod;
        });

        $scope.edit = function (doctor) {
            debugger;
            var doctor = angular.copy(doctor);
            
            doctorService.getById(doctor.ID).success(function (model) {
                debugger;
                $scope.model.Doctor = model.Doctor;
                $scope.model.Address = model.Address;
                $scope.model.Contact = model.Contact;
            });

            //$scope.model.Doctor = doctor; //angular.copy(doctor);
            $scope.model.Mode = "Edit";
            angular.element('#doctorCode').focus();
        };

        $scope.cancel = function () {
            debugger;
            doctorService.getBlank().success(function (model) {
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
                    $scope.model.Doctor.CityCode = '';
                $scope.model.Address.AreaCode = '';

            });
        }

        $scope.LoadAreas = function (cityCode) {
            debugger;
            areaService.getByCity(cityCode).success(function (model) {
                debugger;
                $scope.model.Areas = model;
                if (model.length == 0)
                    $scope.model.Doctor.AreaCode = '';
            });
        }

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updateDoctor();
            } else {
                creatDoctor();
            }
        };

        var creatDoctor = function () {
            debugger;
            doctorService.create($scope.model).success(function (mod) {
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

        var updateDoctor = function () {
            debugger;
            doctorService.update($scope.model).success(function (mod) {
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

        $scope.delete = function (doctor) {
            if (window.confirm("Are you sure, you want to delete the record?")) {
                debugger;
                $scope.model.Doctor = doctor;
                $scope.model.IsActive = false;
                doctorService.update($scope.model).success(function (mod) {
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

    doctorCtrl.$inject = ["$scope", "$log", "$routeParams", "doctorService", "areaService", "cityService", "ngToast"];
    app.controller("doctorCtrl", doctorCtrl);

}(angular.module("QST")));