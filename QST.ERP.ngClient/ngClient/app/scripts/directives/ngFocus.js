'use strict';
(function (app) {

    var ngFocus = function () {
        return {
            link: function (scope, element, attrs) {
                element[0].focus();
            }
        };
    };

    app.directive("ngFocus", ngFocus);

}(angular.module("QST")))
