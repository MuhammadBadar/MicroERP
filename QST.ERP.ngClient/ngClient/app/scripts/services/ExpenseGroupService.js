(function (app) {
    debugger;

    var expenseGroupService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetAllExpenseGroups/");
        };

        var get = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetExpenseGroupViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetExpenseGroup");
        };

        var create = function (ExpenseGroup) {
            return $http.post(apiUrl + "BDM/AddExpenseGroup/", ExpenseGroup);
        }

        var update = function (expenseGroup) {
            return $http.post(apiUrl + "BDM/ModifyExpenseGroup/", expenseGroup)
        }


        return {
            create: create,
            update: update,
            getAll: getAll,
            get: get,
            getBlank: getBlank
        };
    };

    expenseGroupService.$inject = ["$http", "apiUrl"];
    app.factory("expenseGroupService", expenseGroupService);

}(angular.module("QST")))
