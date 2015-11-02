'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */
angular.module('MainApp')
	.directive('header',function(){
		return {
		    templateUrl: '/app/dashboard/scripts/directives/header/header.html',
        restrict: 'E',
        replace: true,
    	}
	});


