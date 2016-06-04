'use strict';

(function (app) {
    debugger;
    var config = function ($routeProvider, $locationProvider) {
        $routeProvider
          .when('/GroceryKitForm', {
                templateUrl: '/ngClient/app/views/GroceryForm.html',
                controller: 'groceryKitCtrl'
            })
          .when('/', {
                templateUrl: '/ngClient/app/views/_main.html',
                controller: 'mainCtrl'
            })           
          .when('/Region', {
                templateUrl: '/ngClient/app/views/Region.html',
                controller: 'regionCtrl'
          })
          .when('/City', {
                templateUrl: '/ngClient/app/views/City.html',
                controller: 'cityCtrl'
          })
          .when('/Area', {
              templateUrl: '/ngClient/app/views/Area.html',
              controller: 'areaCtrl'
          })
          .when('/Manager', {
                templateUrl: '/ngClient/app/views/Manager.html',
                controller: 'managerCtrl'
          })
          .when('/Doctor', {
                templateUrl: '/ngClient/app/views/Doctor.html',
                controller: 'doctorCtrl'
          })
          .when('/ExpenseGroup', {
                templateUrl: '/ngClient/app/views/ExpenseGroup.html',
                controller: 'expenseGroupCtrl'
          })
          .when('/ChartOfAccount', {
              templateUrl: '/ngClient/app/views/ChartOfAccount.html',
              controller: 'ChartOfAccountCtrl'
          })
          .when('/GiftType', {
                templateUrl: '/ngClient/app/views/GiftType.html',
                controller: 'giftTypeCtrl'
          })
          .when('/Donor', {
                templateUrl: '/ngClient/app/views/Donor.html',
                controller: 'donorCtrl'
          })
          .when('/Voucher', {
                templateUrl: '/ngClient/app/views/Voucher.html',
                controller: 'voucherCtrl'
          })
          .when('/Employee', {
                templateUrl: '/ngClient/app/views/Employee.html',
                controller: 'employeeCtrl'
          })
          .when('/Bank', {
               templateUrl: '/ngClient/app/views/Bank.html',
               controller: 'bankCtrl'
           })
          .otherwise({
              redirectTo: '/Project'
          });
      
    };
    config.$inject = ['$routeProvider', '$locationProvider'];

    app.config(config);
    app.constant("apiUrl", "/api/");
}(angular.module("QST", ["ngRoute", "ngMessages", "ngResource", "ngAnimate", "ngSanitize", "ui.bootstrap", "angularUtils.directives.dirPagination", "ngToast", "autocomplete"])));


