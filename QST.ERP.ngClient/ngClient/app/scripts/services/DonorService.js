(function (app) {
    debugger;

    var donorService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "AlKhair/GetAllDonors/");
        };

        var getByRegion = function (regionCode) {
            return $http.get(apiUrl + "AlKhair/GetDonorsByRegion?regionCode=" + regionCode)
        }

        var get = function () {
            debugger;
            return $http.get(apiUrl + "AlKhair/GetDonorViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "AlKhair/GetDonor");
        };

        var create = function (donor) {
            return $http.post(apiUrl + "AlKhair/AddDonor/", donor);
        }

        var update = function (donor) {
            return $http.post(apiUrl + "AlKhair/ModifyDonor/", donor)
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

    donorService.$inject = ["$http", "apiUrl"];
    app.factory("donorService", donorService);

}(angular.module("QST")))
