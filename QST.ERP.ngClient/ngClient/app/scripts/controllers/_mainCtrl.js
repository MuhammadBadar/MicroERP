'use strict';

//QST.controller('mainCtrl', function($scope) {
//  $scope.awesomeThings = [
//    'HTML5 Boilerplate',
//    'AngularJS',
//    'Testacular'
//  ];
//});

(function (app) {

    var mainCtrl = function ($scope, $routeParams, $window) { 
        debugger;
    };

    mainCtrl.$inject = ["$scope", "$routeParams", "$window"];

    app.controller("mainCtrl", mainCtrl);

}(angular.module("QST")));
