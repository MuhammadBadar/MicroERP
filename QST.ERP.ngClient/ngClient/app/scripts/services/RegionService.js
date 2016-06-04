(function (app) {
    debugger;

    var regionService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetAllRegions/");
        };

        var get = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetRegionViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetRegion");
        };

        var create = function (Region) {
            return $http.post(apiUrl + "BDM/AddRegion/", Region);
        }

        var update = function (region) {
            return $http.post(apiUrl + "BDM/ModifyRegion/", region)
        }


        return {
            create: create,
            update: update,
            getAll: getAll,
            get: get,
            getBlank: getBlank
        };
    };

    regionService.$inject = ["$http", "apiUrl"];
    app.factory("regionService", regionService);
    
}(angular.module("QST")))
