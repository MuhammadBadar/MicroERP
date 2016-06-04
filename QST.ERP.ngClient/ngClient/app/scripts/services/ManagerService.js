(function (app) {
    debugger;

    var managerService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetAllManagers/");
        };

        var get = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetManagerViewModel");
        };

        var getById = function (id) {
            debugger;
            //return $http.get(apiUrl + "BDM/GetManagerById");
            return $http.get(apiUrl + "BDM/GetManagerById?managerId=" + id);
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetManager");
        };

        var create = function (Manager) {
            return $http.post(apiUrl + "BDM/AddManager/", Manager);
        }

        var update = function (area) {
            return $http.post(apiUrl + "BDM/ModifyManager/", area)
        }

        return {
            create: create,
            update: update,
            getAll: getAll,
            get: get,
            getById : getById,
            getBlank: getBlank
        };
    };

    managerService.$inject = ["$http", "apiUrl"];
    app.factory("managerService", managerService);

}(angular.module("QST")))
