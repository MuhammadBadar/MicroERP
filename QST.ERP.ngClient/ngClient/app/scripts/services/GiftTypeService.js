(function (app) {
    debugger;

    var giftTypeService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "AlKhair/GetAllGiftTypes/");
        };

        var getByRegion = function (regionCode) {
            return $http.get(apiUrl + "AlKhair/GetGiftTypesByRegion?regionCode=" + regionCode)
        }

        var get = function () {
            debugger;
            return $http.get(apiUrl + "AlKhair/GetGiftTypeViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "AlKhair/GetGiftType");
        };

        var create = function (giftType) {
            return $http.post(apiUrl + "AlKhair/AddGiftType/", giftType);
        }

        var update = function (giftType) {
            return $http.post(apiUrl + "AlKhair/ModifyGiftType/", giftType)
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

    giftTypeService.$inject = ["$http", "apiUrl"];
    app.factory("giftTypeService", giftTypeService);

}(angular.module("QST")))
