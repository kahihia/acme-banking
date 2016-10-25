(function() {

    'use strict';

    angular.module('app').factory('transactionsService', transactionsService);

    transactionsService.$inject = ['$http', 'ACME_API_URL', '$httpParamSerializer'];

    function transactionsService($http, ACME_API_URL, $httpParamSerializer) {
        var service = {
            getAccount: getAccount,
            getTransactions: getTransactions
        };

        function getAccount(id) {
            return $http.get(ACME_API_URL + '/api/accounts/' + id)
        }

        function getTransactions(id, params) {
            var serializedParams = $httpParamSerializer(params);

            return $http.get(ACME_API_URL + '/api/transactions/' + id + '?' + serializedParams)
        }

        return service;
    }

})();
