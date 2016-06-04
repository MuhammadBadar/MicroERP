//app.directive('ngBlur', ['$parse', function ($parse) {
//    return function (scope, element, attr) {
//        var fn = $parse(attr['ngBlur']);
//        element.bind('blur', function (event) {
//            scope.$apply(function () {
//                fn(scope, { $event: event });
//            });
//        });
//    }
//}]);

//'use strict';
(function (app) {

    var ngBlur = function (scope, element, attr) {
        var fn = $parse(attr['ngBlur']);
        element.bind('blur', function (event) {
            scope.$apply(function () {
                fn(scope, { $event: event });
            });
        });
    }

    app.directive("ngBlur", ngBlur);

}(angular.module("QST")))
