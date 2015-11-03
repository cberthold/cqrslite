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

.config(['$ocLazyLoadProvider', 'CustomerApiConfigProvider', function ($ocLazyLoadProvider, CustomerApiConfigProvider) {

    $ocLazyLoadProvider.config({
        debug: true,
        events: true,
    });

    CustomerApiConfigProvider.setApiUri("http://localhost:16463/api/");
}]);