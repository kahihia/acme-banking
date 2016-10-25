(function() {

    'use strict';

    angular.module('app').factory('accountService', accountService);

    accountService.$inject = ['$http', 'ACME_API_URL'];

    function accountService($http, ACME_API_URL) {
        var service = {
            getAccounts: getAccounts
        };

        function getAccounts() {
            return $http.get(ACME_API_URL + '/api/accounts');
        }

        return service;
    }
})();
