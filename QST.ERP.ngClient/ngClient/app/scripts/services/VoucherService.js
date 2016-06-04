(function (app) {
    debugger;

    var voucherService = function ($http, apiUrl) {

        var getAll = function () {
            debugger;
            return $http.get(apiUrl + "AlKhair/GetAllVouchers/");
        };

        var getByRegion = function (regionCode) {
            return $http.get(apiUrl + "AlKhair/GetVouchersByRegion?regionCode=" + regionCode)
        }

        var get = function () {
            debugger;
            return $http.get(apiUrl + "AlKhair/GetVoucherViewModel");
        };

        var getBlank = function () {
            debugger;
            return $http.get(apiUrl + "AlKhair/GetVoucher");
        };

        var create = function (voucher) {
            return $http.post(apiUrl + "AlKhair/AddVoucher/", voucher);
        }

        var update = function (voucher) {
            return $http.post(apiUrl + "AlKhair/ModifyVoucher/", voucher)
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

    voucherService.$inject = ["$http", "apiUrl"];
    app.factory("voucherService", voucherService);

}(angular.module("QST")))
