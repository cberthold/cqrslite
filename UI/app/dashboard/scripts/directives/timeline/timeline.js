'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */
angular.module('MainApp')
	.directive('timeline',function() {
    return {
        templateUrl: '/app/dashboard/scripts/directives/timeline/timeline.html',
        restrict: 'E',
        replace: true,
    }
  });
