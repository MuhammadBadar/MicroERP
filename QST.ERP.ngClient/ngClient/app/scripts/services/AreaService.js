(function (app) {
    debugger;

    var areaService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetAllAreas/");
        };

        var getByCity = function (cityCode) {
            debugger;
            return $http.get(apiUrl + "BDM/GetAreasByCity?cityCode=" + cityCode);
        }

        var get = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetAreaViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetArea");
        };

        var create = function (Area) {
            return $http.post(apiUrl + "BDM/AddArea/", Area);
        }

        var update = function (area) {
            return $http.post(apiUrl + "BDM/ModifyArea/", area)
        }


        return {
            create: create,
            update: update,
            getAll: getAll,
            get: get,
            getBlank: getBlank,
            getByCity: getByCity
        };
    };

    areaService.$inject = ["$http", "apiUrl"];
    app.factory("areaService", areaService);

}(angular.module("QST")))
