(function (app) {
    //debugger;
    var donorCtrl = function ($scope, $log, $routeParams, donorService, ngToast) {

        donorService.getAll()
                            .success(function (mod) {
                                $scope.model = mod;
                            });

        $scope.edit = function (donor) {
            debugger;
            $scope.model.Donor = angular.copy(donor);
            $scope.model.Mode = "Edit";
            angular.element('#donorName').focus();
        };

        $scope.cancel = function () {
            debugger;
            donorService.getBlank().success(function (model) {
                debugger;
                $scope.model.Donors = model;
            });
        };

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updatedonor();
            } else {
                createdonor();
            }
        };

        var createdonor = function () {
            debugger;
            donorService.create($scope.model).success(function (mod) {
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

        var updatedonor = function () {
            debugger;
            donorService.update($scope.model.Donor).success(function (mod) {
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
                donorService.update(model)
                    .success(function (mod) {
                        alert(mod.Message);
                        $scope.model = mod;
                        angular.element('#' + mod.FieldId).focus();
                    });
            }
        };
    };

    donorCtrl.$inject = ["$scope", "$log", "$routeParams", "donorService", "ngToast"];
    app.controller("donorCtrl", donorCtrl);

}(angular.module("QST")));