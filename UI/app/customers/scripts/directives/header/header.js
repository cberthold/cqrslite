'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */
angular.module('customersModule')
	.directive('header',function(){
		return {
		    templateUrl: '/app/customers/scripts/directives/header/header.html',
        restrict: 'E',
        replace: true,
    	}
	});


