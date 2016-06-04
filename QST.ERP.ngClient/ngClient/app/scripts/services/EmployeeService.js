(function (app) {
    debugger;

    var employeeService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetAllEmployeeCores/");
        };

        var getByRegion = function (regionCode) {
            return $http.get(apiUrl + "BDM/GetEmployeeCoresByRegion?regionCode=" + regionCode)
        }

        var get = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetEmployeeCoreViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetEmployeeCore");
        };

        var create = function (employee) {
            return $http.post(apiUrl + "BDM/AddEmployeeCore/", employee);
        }

        var update = function (employee) {
            return $http.post(apiUrl + "BDM/ModifyEmployeeCore/", employee)
        }

        return {
            create: create,
            update: update,
            getAll: getAll,
            get: get,
            getBlank: getBlank,
            getByRegion: getByRegion
        };
    };

    employeeService.$inject = ["$http", "apiUrl"];
    app.factory("employeeService", employeeService);

}(angular.module("QST")))
