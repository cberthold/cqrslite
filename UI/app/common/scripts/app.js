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
    'customersModule',
    'adminModule'
    
  ])

.config(['$ocLazyLoadProvider', 'AdminApiConfigProvider', 'CustomerApiConfigProvider', '$provide', function ($ocLazyLoadProvider, AdminApiConfigProvider, CustomerApiConfigProvider, $provide) {

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

    AdminApiConfigProvider.setApiUri("http://localhost:16463/api/");
    CustomerApiConfigProvider.setApiUri("http://localhost:16463/api/");
}]);