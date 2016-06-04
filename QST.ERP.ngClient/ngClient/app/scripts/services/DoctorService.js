(function (app) {
    debugger;

    var doctorService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetAllDoctors/");
        };

        var get = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetDoctorViewModel");
        };

        var getById = function (id) {
            debugger;
            //return $http.get(apiUrl + "BDM/GetDoctorById");
            return $http.get(apiUrl + "BDM/GetDoctorById?doctorId=" + id);
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetDoctor");
        };

        var create = function (Doctor) {
            return $http.post(apiUrl + "BDM/AddDoctor/", Doctor);
        }

        var update = function (area) {
            return $http.post(apiUrl + "BDM/ModifyDoctor/", area)
        }

        return {
            create: create,
            update: update,
            getAll: getAll,
            get: get,
            getById: getById,
            getBlank: getBlank
        };
    };

    doctorService.$inject = ["$http", "apiUrl"];
    app.factory("doctorService", doctorService);

}(angular.module("QST")))
