(function() {

    'use strict';

    angular.module('app').controller('AccountDetailsController', AccountDetailsController);

    AccountDetailsController.$inject = ['accountDetailsService', '$stateParams'];

    function AccountDetailsController(accountDetailsService, $stateParams) {
        var vm = this;
        vm.account = {};

        activate();

        function activate() {
            accountDetailsService.getAccount($stateParams.id)
                .then(function(response) {
                    vm.account = response.data;
                    return accountDetailsService.getTransactions(vm.account.id);
                })
                .then(function(response) {
                    vm.account.transactions = response.data;
                })
                .catch(function(err) {
                    console.log(err);
                });
        }
    }
    
})();
