'use strict';
/**
 * @ngdoc overview
 * @name MainApp
 * @description
 * # MainApp
 *
 * Main module of the application.
 */
angular
  .module('MainApp', [
    'ng',
    'oc.lazyLoad',
    'dashboardModule',
    'customersModule'
    
  ])

.config(['$ocLazyLoadProvider', 'CustomerApiConfigProvider', '$provide', function ($ocLazyLoadProvider, CustomerApiConfigProvider, $provide) {

    $provide.decorator('$state', function ($delegate, $stateParams) {
        $delegate.forceReload = function () {
            return $delegate.go($delegate.current, $stateParams, {
                reload: true,
                inherit: false,
                notify: true
            });
        };

        return $delegate;
    });

    $ocLazyLoadProvider.config({
        //debug: true,
        //events: true,
    });

    CustomerApiConfigProvider.setApiUri("http://localhost:16463/api/");
}]);