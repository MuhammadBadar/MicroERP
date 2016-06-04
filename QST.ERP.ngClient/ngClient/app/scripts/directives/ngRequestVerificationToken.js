'use strict';
(function (app) {
    debugger;
    var ngRequestVerificationToken = function ($http) {
        debugger;
        return function (scope, element, attrs) {
            debugger;

            $http.defaults.headers.common['RequestVerificationToken'] = attrs.ngRequestVerificationToken || "no request verification token";
            //$http.defaults.headers.common['RequestVerificationToken'] = angular.element("dryCleanPnDAppDiv").attr('ng-request-verification-token');

        };
    };

    ngRequestVerificationToken.$inject = ["$http"];
    app.directive("ngRequestVerificationToken", ngRequestVerificationToken);

}(angular.module("QST")))
