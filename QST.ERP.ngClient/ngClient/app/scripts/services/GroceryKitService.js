(function (app) {
    debugger;

    var groceryKitService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "GroceryKit/GetAllGroceryKits/");
        };

        var get = function () {
            debugger;
            return $http.get(apiUrl + "GroceryKit/ManageGroceryKit");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "GroceryKit/GetGroceryKit");
        };

        var create = function (GroceryKit) {
            return $http.post(apiUrl + "GroceryKit/AddGroceryKit/", GroceryKit);
        }

        var update = function (expenseGroup) {
            return $http.post(apiUrl + "GroceryKit/ModifyGroceryKit/", expenseGroup)
        }


        return {
            create: create,
            update: update,
            getAll: getAll,
            get: get,
            getBlank: getBlank
        };
    };

    groceryKitService.$inject = ["$http", "apiUrl"];
    app.factory("groceryKitService", groceryKitService);

}(angular.module("QST")))
