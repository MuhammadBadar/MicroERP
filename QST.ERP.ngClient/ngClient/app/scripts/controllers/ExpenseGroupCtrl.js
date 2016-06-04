(function (app) {
    //debugger;
    var expenseGroupCtrl = function ($scope, $log, $routeParams, expenseGroupService, ngToast) {

        expenseGroupService.getAll()
                        .success(function (mod) {
                            $scope.model = mod;
                            //$scope.model.Mode = "Add";
                        });

        $scope.edit = function (expenseGroup) {
            debugger;
            $scope.model.ExpenseGroup = angular.copy(expenseGroup);
            $scope.model.Mode = "Edit";
            angular.element('#groupCode').focus();
        };


        $scope.cancel = function () {
            debugger;
            expenseGroupService.getBlank().success(function (model) {
                debugger;
                $scope.model.Address = model;
            });
        };

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updateExpenseGroup();
            } else {
                creatExpenseGroup();
            }
        };

        var creatExpenseGroup = function () {
            debugger;
            expenseGroupService.create($scope.model).success(function (mod) {
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

        var updateExpenseGroup = function () {
            debugger;
            expenseGroupService.update($scope.model.ExpenseGroup).success(function (mod) {
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
                expenseGroupService.update(model)
                    .success(function (mod) {
                        alert(mod.Message);
                        $scope.model = mod;
                        angular.element('#' + mod.FieldId).focus();
                    });
            }
        };
    };

    expenseGroupCtrl.$inject = ["$scope", "$log", "$routeParams", "expenseGroupService", "ngToast"];
    app.controller("expenseGroupCtrl", expenseGroupCtrl);

}(angular.module("QST")));