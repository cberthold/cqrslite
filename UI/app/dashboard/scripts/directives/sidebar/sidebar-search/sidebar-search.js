'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */

angular.module('MainApp')
  .directive('sidebarSearch',function() {
    return {
        templateUrl: '/app/dashboard/scripts/directives/sidebar/sidebar-search/sidebar-search.html',
      restrict: 'E',
      replace: true,
      scope: {
      },
      controller: function ($scope) {
          var vm = this;
          vm.selectedMenu = 'home';

          
          vm.search = function () {
              alert(vm.searchText + ' in dashboard');
              vm.reset();
          };

          vm.reset = function () {
              vm.searchText = '';
          };

          vm.reset();

      },
      controllerAs: 'vm'

    }
  });
