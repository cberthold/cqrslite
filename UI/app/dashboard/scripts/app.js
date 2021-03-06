'use strict';
/**
 * @ngdoc overview
 * @name dashboardModule
 * @description
 * # dashboardModule
 *
 * dashboard module of the application.
 */
angular
  .module('dashboardModule', [
    'oc.lazyLoad',
    'ui.router',
    'ui.bootstrap',
    'angular-loading-bar'
  ])
  .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {

      
      $urlRouterProvider.otherwise('/dashboard/home');

      $stateProvider
        
        .state('dashboard', {
            url: '/dashboard',
            templateUrl: '/app/dashboard/views/dashboard/main.html',
            resolve: {
                loadMyDirectives: function ($ocLazyLoad) {
                    return $ocLazyLoad.load(
                    {
                        name: 'dashboardModule',
                        files: [
                        '/app/dashboard/scripts/directives/header/header.js',
                        '/app/dashboard/scripts/directives/header/header-notification/header-notification.js',
                        '/app/dashboard/scripts/directives/sidebar/sidebar.js',
                        '/app/dashboard/scripts/directives/sidebar/sidebar-search/sidebar-search.js'
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
        .state('dashboard.home', {
            url: '/home',
            controller: 'MainCtrl',
            templateUrl: '/app/dashboard/views/dashboard/home.html',
            resolve: {
                loadMyFiles: function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'dashboardModule',
                        files: [
                        '/app/dashboard/scripts/controllers/main.js',
                        '/app/dashboard/scripts/directives/timeline/timeline.js',
                        '/app/dashboard/scripts/directives/notifications/notifications.js',
                        '/app/dashboard/scripts/directives/chat/chat.js',
                        '/app/dashboard/scripts/directives/dashboard/stats/stats.js'
                        ]
                    })
                }
            }
        })

        .state('dashboard.form', {
            templateUrl: '/app/dashboard/views/form.html',
            url: '/form'
        })
        .state('dashboard.blank', {
            templateUrl: '/app/dashboard/views/pages/blank.html',
            url: '/blank'
        })
        .state('login', {
            templateUrl: '/app/dashboard/views/pages/login.html',
            url: '/login'
        })
        .state('dashboard.chart', {
            templateUrl: '/app/dashboard/views/chart.html',
            url: '/chart',
            controller: 'ChartCtrl',
            resolve: {
                loadMyFile: function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'chart.js',
                        files: [
                          'bower_components/angular-chart.js/dist/angular-chart.min.js',
                          'bower_components/angular-chart.js/dist/angular-chart.css'
                        ]
                    }),
                    $ocLazyLoad.load({
                        name: 'dashboardModule',
                        files: ['/app/dashboard/scripts/controllers/chartContoller.js']
                    })
                }
            }
        })
        .state('dashboard.table', {
            templateUrl: '/app/dashboard/views/table.html',
            url: '/table'
        })
        .state('dashboard.panels-wells', {
            templateUrl: '/app/dashboard/views/ui-elements/panels-wells.html',
            url: '/panels-wells'
        })
        .state('dashboard.buttons', {
            templateUrl: '/app/dashboard/views/ui-elements/buttons.html',
            url: '/buttons'
        })
        .state('dashboard.notifications', {
            templateUrl: '/app/dashboard/views/ui-elements/notifications.html',
            url: '/notifications'
        })
        .state('dashboard.typography', {
            templateUrl: '/app/dashboard/views/ui-elements/typography.html',
            url: '/typography'
        })
        .state('dashboard.icons', {
            templateUrl: '/app/dashboard/views/ui-elements/icons.html',
            url: '/icons'
        })
        .state('dashboard.grid', {
            templateUrl: '/app/dashboard/views/ui-elements/grid.html',
            url: '/grid'
        })
  }]);


