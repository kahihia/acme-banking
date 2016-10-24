(function() {

    'use strict';

    angular.module('app').factory('accountService', accountService);

    accountService.$inject = ['$http'];

    function accountService($http) {
        var service = {
            getAccounts: getAccounts
        };

        function getAccounts() {
            console.log('getAccounts');
            return $http.get('http://localhost:5000/api/accounts');
        }

        return service;
    }
})();
