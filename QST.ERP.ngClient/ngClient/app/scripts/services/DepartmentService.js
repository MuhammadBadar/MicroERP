(function (app) {
    debugger;

    var departmentService = function ($http, apiUrl) {
        
        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "/GetAllProjects/");
        };

        var get = function () {
            debugger;
            return $http.get(apiUrl + "/GetProjectViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "/GetProject");
        };

        var create = function (Project) {
            return $http.post(apiUrl + "/AddProject/", Project);
        }

        return {
            create: create,
            getAll: getAll,
            get: get,
            getBlank: getBlank
        };
    };

    departmentService.$inject = ["$http", "apiUrl"];
    app.factory("departmentService", departmentService);


}(angular.module("QST")))
