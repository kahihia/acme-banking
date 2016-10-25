(function() {

    'use strict';

    angular.module('app').controller('TransactionsController', TransactionsController);

    TransactionsController.$inject = ['$scope', 'transactionsService', '$stateParams'];

    function TransactionsController($scope, transactionsService, $stateParams) {
        var vm = this;

        vm.endOfTransactions = false;
        vm.pagination = {
            limit: 10,
            offset: 0
        };

        vm.loadTransactions = loadTransactions;
        vm.account = {};

        activate();

        function activate() {
            transactionsService.getAccount($stateParams.id)
                .then(function(response) {
                    vm.account = response.data;
                    return transactionsService.getTransactions(vm.account.id, vm.pagination);
                })
                .then(function(response) {
                    vm.account.transactions = response.data;

                    // increment offset.
                    vm.pagination.offset = vm.account.transactions.length + 1;

                    $scope.$broadcast('scroll.infiniteScrollComplete');
                })
                .catch(function(err) {
                    console.log(err);
                });
        }

        // Load more transactions on scroll.
        function loadTransactions() {
            if (!vm.account.transactions) return;

            // increment offset.
            vm.pagination.offset = vm.account.transactions.length + 1;

            transactionsService.getTransactions(vm.account.id, vm.pagination)
                .then(function(response) {

                    if (response.data.length == 0) {
                        // hide infinite-scroller if we don't get any records back.
                        vm.endOfTransactions = true;
                    }

                    angular.forEach(response.data, function(value, key) {
                        vm.account.transactions.push(value);
                    });

                    $scope.$broadcast('scroll.infiniteScrollComplete');
                })
                .catch(function(err) {
                    console.log(err);

                    $scope.$broadcast('scroll.infiniteScrollComplete');
                });
        }
    }

})();
