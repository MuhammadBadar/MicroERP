(function (app) {
    debugger;

    var bankService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetAllBanks/");
        };

        var getByRegion = function (regionCode) {
            return $http.get(apiUrl + "BDM/GetBanksByRegion?regionCode=" + regionCode)
        }

        var get = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetBankViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "BDM/GetBank");
        };

        var create = function (bank) {
            return $http.post(apiUrl + "BDM/AddBank/", bank);
        }

        var update = function (bank) {
            return $http.post(apiUrl + "BDM/ModifyBank/", bank)
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

    bankService.$inject = ["$http", "apiUrl"];
    app.factory("bankService", bankService);

}(angular.module("QST")))
