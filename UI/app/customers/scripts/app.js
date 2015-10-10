'use strict';
/**
 * @ngdoc overview
 * @name customersModule
 * @description
 * # customersModule
 *
 * Main module of the application.
 */
angular
  .module('customersModule', [
    'oc.lazyLoad',
    'ui.router',
    'ui.bootstrap',
    'angular-loading-bar',
    'sbAdminApp'
  ])
  .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {

      $stateProvider
        .state('customers', {
            url: '/customers',
            templateUrl: '/app/customers/views/main.html',
            resolve: {
                loadMyDirectives: function ($ocLazyLoad) {
                    return $ocLazyLoad.load(
                    {
                        name: 'customersModule',
                        files: [
                        '/app/customers/scripts/directives/header/header.js',
                        '/app/customers/scripts/directives/header/header-notification/header-notification.js',
                        '/app/customers/scripts/directives/sidebar/sidebar.js',
                        '/app/customers/scripts/directives/sidebar/sidebar-search/sidebar-search.js'
                        ]
                    }),
                    $ocLazyLoad.load(
                    {
                        name: 'toggle-switch',
                        files: ["bower_components/angular-toggle-switch/angular-toggle-switch.min.js",
                               "bower_components/angular-toggle-switch/angular-toggle-switch.css"
                        ]
                    }),
                    $ocLazyLoad.load(
                    {
                        name: 'ngAnimate',
                        files: ['bower_components/angular-animate/angular-animate.js']
                    })
                    $ocLazyLoad.load(
                    {
                        name: 'ngCookies',
                        files: ['bower_components/angular-cookies/angular-cookies.js']
                    })
                    $ocLazyLoad.load(
                    {
                        name: 'ngResource',
                        files: ['bower_components/angular-resource/angular-resource.js']
                    })
                    $ocLazyLoad.load(
                    {
                        name: 'ngSanitize',
                        files: ['bower_components/angular-sanitize/angular-sanitize.js']
                    })
                    $ocLazyLoad.load(
                    {
                        name: 'ngTouch',
                        files: ['bower_components/angular-touch/angular-touch.js']
                    })
                }
            }
        })
          .state('customers.add', {
              templateUrl: '/app/customers/views/add.html',
              url: '/add',
              controller: 'CustomerAddController',
              resolve: {
                  loadMyFile: function ($ocLazyLoad) {
                      return $ocLazyLoad.load({
                          name: 'customersModule',
                          files: ['/app/customers/scripts/controllers/customer-add.js']
                      })
                  }
              }
          })
        .state('customers.list', {
            templateUrl: '/app/customers/views/list.html',
            url: '/list'
        })
        
  }]);


