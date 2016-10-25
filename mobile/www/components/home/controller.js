(function() {

    'use strict';

    angular.module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = [];

    function HomeController() {
        var vm = this;
        vm.title = 'Home';

        vm.labels2 = ['Entertainment', 'Medical', 'Membership', 'Fuel', 'Utilities', 'Travel', 'Groceries'];
        vm.data2 = [6818.86, 7612.96, 10059.65, 3276.44, 4188.25, 10433.48, 14235.84 ];

    }

})();
