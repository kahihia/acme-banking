angular.module('app', ['ionic', 'chart.js'])
// For the base API URL, we're using the proxy defined in the ionic.config.json
.constant('ACME_API_URL', '')
.run(function($ionicPlatform) {
        $ionicPlatform.ready(function() {
            // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
            // for form inputs)
            if (window.cordova && window.cordova.plugins.Keyboard) {
                cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
                cordova.plugins.Keyboard.disableScroll(true);

            }
            if (window.StatusBar) {
                // org.apache.cordova.statusbar required
                StatusBar.styleDefault();
            }
        });
    })
    .filter('accountType', function() {
        return function(input) {
            return input == 1 ? 'Checking' : 'Savings';
        }
    })
    .config(function($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('app', {
                url: '/app',
                abstract: true,
                templateUrl: 'components/shared/menu.html',
                controller: 'AppController'
            })
            .state('app.accounts', {
                url: '/accounts',
                cache: false,
                views: {
                    'menuContent': {
                        templateUrl: 'components/accounts/accounts.html',
                        controller: 'AccountController',
                        controllerAs: 'vm'
                    }
                }
            })
            .state('app.transactions', {
                url: '/transactions/:id',
                cache: false,
                views: {
                    'menuContent': {
                        templateUrl: 'components/accounts/transactions/transactions.html',
                        controller: 'TransactionsController',
                        controllerAs: 'vm'
                    }
                }
            })
            .state('app.home', {
                url: '/home',
                views: {
                    'menuContent': {
                        templateUrl: 'components/home/home.html',
                        controller: 'HomeController',
                        controllerAs: 'vm'
                    }
                }
            });

        // if none of the above states are matched, use this as the fallback
        $urlRouterProvider.otherwise('/app/home');
    });
