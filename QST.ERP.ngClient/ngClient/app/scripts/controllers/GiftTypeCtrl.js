(function (app) {
    //debugger;
    var giftTypeCtrl = function ($scope, $log, $routeParams, giftTypeService, ngToast) {

        giftTypeService.getAll()
                            .success(function (mod) {
                                $scope.model = mod;
                            });

        $scope.edit = function (giftType) {
            debugger;
            $scope.model.GiftType = angular.copy(giftType);
            $scope.model.Mode = "Edit";
            angular.element('#giftTypeCode').focus();
        };

        $scope.cancel = function () {
            debugger;
            giftTypeService.getBlank().success(function (model) {
                debugger;
                $scope.model.GiftTypes = model;
            });
        };

        $scope.save = function () {
            if ($scope.model.Mode == "Edit") {
                updateGiftType();
            } else {
                createGiftType();
            }
        };

        var createGiftType = function () {
            debugger;
            giftTypeService.create($scope.model).success(function (mod) {
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

        var updateGiftType = function () {
            debugger;
            giftTypeService.update($scope.model.GiftType).success(function (mod) {
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
                giftTypeService.update(model)
                    .success(function (mod) {
                        alert(mod.Message);
                        $scope.model = mod;
                        angular.element('#' + mod.FieldId).focus();
                    });
            }
        };
    };

    giftTypeCtrl.$inject = ["$scope", "$log", "$routeParams", "giftTypeService", "ngToast"];
    app.controller("giftTypeCtrl", giftTypeCtrl);

}(angular.module("QST")));