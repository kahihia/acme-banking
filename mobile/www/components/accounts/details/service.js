(function() {

    'use strict';

    angular.module('app').factory('accountDetailsService', accountDetailsService);

    accountDetailsService.$inject = ['$http', 'ACME_API_URL'];

    function accountDetailsService($http, ACME_API_URL) {
        var service = {
          getAccount: getAccount,
            getTransactions: getTransactions
        };

        function getAccount(id) {
            return $http.get(ACME_API_URL + '/api/accounts/' + id)
        }

        function getTransactions(id) {
            return $http.get(ACME_API_URL + '/api/transactions/' + id)
        }

        return service;
    }

})();
