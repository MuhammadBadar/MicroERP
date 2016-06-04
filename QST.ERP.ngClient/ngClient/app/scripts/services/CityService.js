(function (app) {
    debugger;

    var cityService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetAllCities/");
        };

        var getByRegion = function (regionCode) {
            return $http.get(apiUrl + "BDM/GetCitiesByRegion?regionCode="+ regionCode)
        }

        var get = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetCityViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetCity");
        };

        var create = function (City) {
            return $http.post(apiUrl + "BDM/AddCity/", City);
        }

        var update = function (city) {
            return $http.post(apiUrl + "BDM/ModifyCity/", city)
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

    cityService.$inject = ["$http", "apiUrl"];
    app.factory("cityService", cityService);

}(angular.module("QST")))
