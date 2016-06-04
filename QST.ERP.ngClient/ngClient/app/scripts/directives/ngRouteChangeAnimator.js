'use strict';
(function (app) {
    var ngRouteChangeAnimator = function ($rootScope) {
        return {
            link: function (scope, element, attrs) {
                element.addClass('hide');

                $rootScope.$on('$routeChangeStart', function () {
                    element.removeClass('hide');
                });

                $rootScope.$on('$routeChangeSuccess', function () {
                    element.addClass('hide');
                });
            }
        };
    };

    app.directive("ngRouteChangeAnimator", ngRouteChangeAnimator);

}(angular.module("QST")))


