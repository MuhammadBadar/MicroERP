(function (app) {
    //debugger;
    var bankCtrl = function ($scope, $log, $routeParams, bankService, ngToast) {

        bankService.getAll()
                            .success(function (mod) {
                                $scope.model = mod;
                            });

        $scope.edit = function (bank) {
            debugger;
            $scope.model.Bank = angular.copy(bank);
            $scope.model.Mode = "Edit";
            angular.element('#bankCode').focus();
        };

        $scope.cancel = function () {
            debugger;
            bankService.getBlank().success(function (model) {
                debugger;
                $scope.model.Banks = model;
            });
        };

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updateBank();
            } else {
                createBank();
            }
        };

        var createBank = function () {
            debugger;
            bankService.create($scope.model).success(function (mod) {
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

        var updateBank = function () {
            debugger;
            bankService.update($scope.model.Bank).success(function (mod) {
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
                bankService.update(model)
                    .success(function (mod) {
                        alert(mod.Message);
                        $scope.model = mod;
                        angular.element('#' + mod.FieldId).focus();
                    });
            }
        };
    };

    bankCtrl.$inject = ["$scope", "$log", "$routeParams", "bankService", "ngToast"];
    app.controller("bankCtrl", bankCtrl);

}(angular.module("QST")));