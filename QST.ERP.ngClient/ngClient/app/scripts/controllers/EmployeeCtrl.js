(function (app) {
    //debugger;
    var employeeCtrl = function ($scope, $log, $routeParams, employeeService, ngToast) {

        employeeService.getAll()
                            .success(function (mod) {
                                $scope.model = mod;
                            });

        $scope.edit = function (employee) {
            debugger;
            $scope.model.EmployeeCore = angular.copy(employee);
            $scope.model.Mode = "Edit";
            angular.element('#employeeName').focus();
        };

        $scope.cancel = function () {
            debugger;
            employeeService.getBlank().success(function (model) {
                debugger;
                $scope.model.EmployeeCores = model;
            });
        };

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updateemployee();
            } else {
                createemployee();
            }
        };

        var createemployee = function () {
            debugger;
            employeeService.create($scope.model).success(function (mod) {
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

        var updateemployee = function () {
            debugger;
            employeeService.update($scope.model.EmployeeCore).success(function (mod) {
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
                employeeService.update(model)
                    .success(function (mod) {
                        alert(mod.Message);
                        $scope.model = mod;
                        angular.element('#' + mod.FieldId).focus();
                    });
            }
        };
    };

    employeeCtrl.$inject = ["$scope", "$log", "$routeParams", "employeeService", "ngToast"];
    app.controller("employeeCtrl", employeeCtrl);

}(angular.module("QST")));